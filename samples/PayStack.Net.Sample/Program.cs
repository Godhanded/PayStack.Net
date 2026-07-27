using PayStack.Net;
using PayStack.Net.Models.Requests;
using PayStack.Net.Models.Common;
using PayStack.Net.Webhooks;

var builder = WebApplication.CreateBuilder(args);

// Registers IPayStackClient plus every resource client, wired with the resilient/authenticated
// HttpClient pipeline. Secret key is read from appsettings.json -> "PayStack:SecretKey" (or the
// PayStack__SecretKey environment variable / user-secrets in development).
builder.Services.AddPayStack(builder.Configuration);

var app = builder.Build();

// ---------------------------------------------------------------------------------------------
// Example 1: initialize a transaction and hand the customer a checkout URL.
// ---------------------------------------------------------------------------------------------
app.MapPost("/checkout", async (IPayStackClient payStack, CancellationToken ct) =>
{
    var response = await payStack.Transactions.InitializeAsync(new InitializeTransactionRequest
    {
        Amount = "500000", // NGN 5,000.00, in kobo
        Email = "customer@example.com",
        Currency = "NGN",
        Channels = [PayStackChannel.Card, PayStackChannel.BankTransfer],
        CallbackUrl = "https://example.com/paystack/callback",
    }, ct);

    // Refit's ApiResponse<T> never throws for non-2xx responses (see PayStackRefitSettingsFactory) —
    // always check IsSuccessStatusCode / response.Content?.Status explicitly.
    if (!response.IsSuccessStatusCode || response.Content is not { Status: true } body)
    {
        return Results.Problem(
            title: "Failed to initialize transaction",
            detail: response.Content?.Message ?? response.Error?.Message,
            statusCode: (int)response.StatusCode);
    }

    return Results.Ok(new { checkoutUrl = body.Data!.AuthorizationUrl, reference = body.Data.Reference });
});

// ---------------------------------------------------------------------------------------------
// Example 2: server-side verification after redirect. Never trust the client-side redirect alone.
// ---------------------------------------------------------------------------------------------
app.MapGet("/paystack/callback", async (string reference, IPayStackClient payStack, CancellationToken ct) =>
{
    var response = await payStack.Transactions.VerifyAsync(reference, ct);

    if (!response.IsSuccessStatusCode || response.Content?.Data is not { } transaction)
    {
        return Results.Problem("Could not verify transaction.");
    }

    // Charge/verify endpoints return HTTP 200 even for a failed transaction — always inspect status.
    if (transaction.Status != PayStackTransactionStatus.Success)
    {
        return Results.BadRequest(new { transaction.Status, transaction.GatewayResponse });
    }

    return Results.Ok(new { transaction.Reference, transaction.Amount, transaction.Customer?.Email });
});

// ---------------------------------------------------------------------------------------------
// Example 3: webhook endpoint. Signature-verifies the raw body before trusting any payload.
// ---------------------------------------------------------------------------------------------
app.MapPost("/webhooks/paystack", async (HttpRequest request, IPayStackWebhookParser parser, ILogger<Program> logger) =>
{
    request.EnableBuffering();
    var webhookEvent = await request.ReadPayStackWebhookEventAsync(parser);

    if (webhookEvent is null)
    {
        // Invalid/missing signature — do not process, and don't leak why to the caller.
        return Results.Unauthorized();
    }

    switch (webhookEvent.Event)
    {
        case PayStackWebhookEventType.ChargeSuccess:
            var transaction = webhookEvent.GetData<PayStack.Net.Models.Responses.TransactionData>();
            logger.LogInformation("Charge succeeded for reference {Reference}, amount {Amount}", transaction.Reference, transaction.Amount);
            // ... reconcile the order, fulfil, etc. Keep this fast; do slow work on a background queue.
            break;

        case PayStackWebhookEventType.TransferSuccess:
        case PayStackWebhookEventType.TransferFailed:
        case PayStackWebhookEventType.TransferReversed:
            logger.LogInformation("Transfer event {Event} received", webhookEvent.Event);
            break;

        default:
            logger.LogDebug("Unhandled Paystack webhook event {Event}", webhookEvent.Event);
            break;
    }

    // Always ack quickly with 200 — Paystack retries on anything else.
    return Results.Ok();
});

app.Run();
