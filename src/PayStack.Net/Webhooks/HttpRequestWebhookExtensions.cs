using Microsoft.AspNetCore.Http;

namespace PayStack.Net.Webhooks;

/// <summary>
/// ASP.NET Core convenience extensions for handling Paystack webhook endpoints. Optional — the SDK
/// core has no ASP.NET Core dependency, so use <see cref="IPayStackWebhookParser"/> or
/// <see cref="PayStackWebhookSignatureVerifier"/> directly in non-ASP.NET Core hosts (Azure Functions,
/// worker services, etc.).
/// </summary>
public static class HttpRequestWebhookExtensions
{
    /// <summary>
    /// Reads the raw request body, verifies the <c>x-paystack-signature</c> header, and parses the
    /// event envelope in one call. Enable request body buffering (<c>request.EnableBuffering()</c>)
    /// upstream first if anything else in the pipeline also needs to read the body.
    /// </summary>
    /// <param name="request">The inbound webhook request.</param>
    /// <param name="parser">The SDK's webhook parser, resolved from DI.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The parsed event, or <c>null</c> when the signature is missing/invalid or the body is malformed.</returns>
    /// <example>
    /// <code>
    /// app.MapPost("/webhooks/paystack", async (HttpRequest request, IPayStackWebhookParser parser) =>
    /// {
    ///     var evt = await request.ReadPayStackWebhookEventAsync(parser);
    ///     if (evt is null) return Results.Unauthorized();
    ///
    ///     if (evt.Event == PayStackWebhookEventType.ChargeSuccess)
    ///     {
    ///         var transaction = evt.GetData&lt;TransactionData&gt;();
    ///         // ... reconcile order ...
    ///     }
    ///
    ///     return Results.Ok();
    /// });
    /// </code>
    /// </example>
    public static async Task<PayStackWebhookEvent?> ReadPayStackWebhookEventAsync(
        this HttpRequest request,
        IPayStackWebhookParser parser,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(parser);

        request.Body.Position = 0;
        using var reader = new StreamReader(request.Body, leaveOpen: true);
        var rawBody = await reader.ReadToEndAsync(cancellationToken).ConfigureAwait(false);

        var signature = request.Headers.TryGetValue(PayStackWebhookSignatureVerifier.SignatureHeaderName, out var values)
            ? values.ToString()
            : null;

        return parser.Parse(rawBody, signature);
    }
}
