# PayStack.Net

[![CI](https://github.com/Godhanded/PayStack.Net/actions/workflows/ci.yml/badge.svg)](https://github.com/Godhanded/PayStack.Net/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/PayStackDotNet.svg)](https://www.nuget.org/packages/PayStackDotNet)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

An unofficial, fully typed, production-ready .NET SDK for the [Paystack API](https://paystack.com/docs/api/) — transactions, customers, subscriptions, plans, transfers, refunds, disputes, dedicated virtual accounts, terminals, storefronts, webhooks, and everything else under `https://paystack.com/docs/api/`. Not built, maintained, or endorsed by Paystack itself — see [Contributing](CONTRIBUTING.md) if interested in helping maintain it.

Built on [Refit](https://github.com/reactiveui/refit) for typed HTTP clients, `Microsoft.Extensions.Http.Resilience` (Polly v8) for retries/timeouts/circuit breaking, and `System.Text.Json`. Every public method, type, and parameter has XML doc comments, so IntelliSense in Visual Studio/Rider/VS Code shows usage guidance inline while typing.

## Contents

- [Install](#install)
- [Namespaces](#namespaces)
- [Quick start (DI)](#quick-start-di)
- [Quick start (no DI)](#quick-start-no-di)
- [Configuration](#configuration)
- [Error handling — `ApiResponse<T>`, never exceptions for API errors](#error-handling)
- [Resilience: retries, timeouts, circuit breaker](#resilience)
- [Idempotency keys](#idempotency-keys)
- [Resource areas](#resource-areas)
- [Webhooks](#webhooks)
- [Design notes](#design-notes)
- [Contributing, building, and publishing](#contributing-building-and-publishing)

## Install

```bash
dotnet add package PayStackDotNet
```

> The NuGet package id is `PayStackDotNet`, not `PayStack.Net` — that id was already registered by an unrelated package before this project claimed it. The namespace, assembly name (`PayStack.Net`), repo, and every type name are unaffected; only the package id used for `dotnet add package`/`<PackageReference Include="...">` differs.

Targets `net8.0`, `net9.0`, and `net10.0`.

## Namespaces

| Namespace | Contains |
|---|---|
| `PayStack.Net` | `IPayStackClient` (the facade — one property per resource, e.g. `client.Transactions`), `PayStackClient` (`.Create(...)` for non-DI usage, `DisposablePayStackClient`), `ServiceCollectionExtensions` (`AddPayStack`), `PayStackJsonOptions` |
| `PayStack.Net.Configuration` | `PayStackOptions`, `PayStackEnvironment` |
| `PayStack.Net.Resources` | The 29 Refit resource interfaces (`ITransactionsClient`, `ICustomersClient`, `ITransfersClient`, ...). You normally don't reference these by name — you get them off `IPayStackClient` |
| `PayStack.Net.Models.Requests` | Every request DTO — `InitializeTransactionRequest`, `CreatePlanRequest`, `InitiateTransferRequest`, `CreateRefundRequest`, etc. |
| `PayStack.Net.Models.Responses` | Every response DTO — `TransactionData`, `PlanData`, `TransferData`, `RefundData`, etc. |
| `PayStack.Net.Models.Common` | `PayStackResponse<T>`, `PayStackMeta`, shared sub-objects (`AuthorizationData`, `CustomerSummary`), and every `static class PayStack<Thing>` of string constants for enum-like fields (`PayStackChannel`, `PayStackTransactionStatus`, `PayStackPlanInterval`, `PayStackWebhookEventType`, ...) |
| `PayStack.Net.Http` | `PayStackCircuitOpenException`, `PayStackTimeoutException` — the two exception types you'll `catch`. Everything else here is internal plumbing (auth/logging/idempotency handlers) |
| `PayStack.Net.Webhooks` | `IPayStackWebhookParser`, `PayStackWebhookEvent`, `PayStackWebhookSignatureVerifier`, and the `HttpRequestWebhookExtensions.ReadPayStackWebhookEventAsync` extension method |

A typical API-calling file needs:

```csharp
using PayStack.Net;
using PayStack.Net.Models.Requests;
using PayStack.Net.Models.Responses;
using PayStack.Net.Models.Common; // for the PayStackChannel / PayStackTransactionStatus / etc. constants
```

A webhook receiver additionally needs:

```csharp
using PayStack.Net.Webhooks;
```

And anywhere you handle transient failures explicitly:

```csharp
using PayStack.Net.Http; // PayStackCircuitOpenException, PayStackTimeoutException
```

## Quick start (DI)

```csharp
using PayStack.Net;
using PayStack.Net.Models.Requests;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPayStack(options =>
{
    options.SecretKey = builder.Configuration["PayStack:SecretKey"]!;
});

var app = builder.Build();

app.MapPost("/checkout", async (IPayStackClient payStack, CancellationToken ct) =>
{
    var response = await payStack.Transactions.InitializeAsync(new InitializeTransactionRequest
    {
        Amount = "500000", // NGN 5,000.00, in kobo — Paystack amounts are always in the currency's subunit
        Email = "customer@example.com",
    }, ct);

    if (!response.IsSuccessStatusCode || response.Content is not { Status: true } body)
    {
        return Results.Problem(response.Content?.Message ?? response.Error?.Message);
    }

    return Results.Ok(new { checkoutUrl = body.Data!.AuthorizationUrl });
});

app.Run();
```

Or bind straight from configuration:

```csharp
// appsettings.json: { "PayStack": { "SecretKey": "sk_test_..." } }
builder.Services.AddPayStack(builder.Configuration);
```

`IPayStackClient` exposes one property per resource area (`Transactions`, `Customers`, `Subscriptions`, `Transfers`, `Refunds`, ...) — see [Resource areas](#resource-areas).

## Quick start (no DI)

For scripts, console apps, and Azure Functions where wiring up a full `IServiceCollection` is overkill:

```csharp
using PayStack.Net;
using PayStack.Net.Models.Requests;

using var payStack = PayStackClient.Create("sk_test_xxx");

var response = await payStack.Transactions.InitializeAsync(new InitializeTransactionRequest
{
    Amount = "500000",
    Email = "customer@example.com",
});
```

`PayStackClient.Create` spins up a small internal `ServiceProvider` that owns the HTTP pipeline; `using` disposes it along with the underlying `HttpClient`s.

## Configuration

All options live on `PayStackOptions`:

```csharp
builder.Services.AddPayStack(options =>
{
    options.SecretKey = "sk_test_xxx";

    // Optional — see "Environments" below.
    options.BaseUrlOverride = new Uri("https://my-proxy.internal/paystack");

    // Optional — defaults shown.
    options.MaxRetryAttempts = 3;
    options.AttemptTimeout = TimeSpan.FromSeconds(30);
    options.TotalTimeout = TimeSpan.FromSeconds(90);
    options.CircuitBreakerFailureThreshold = 8;
    options.CircuitBreakerBreakDuration = TimeSpan.FromSeconds(30);
    options.AutoGenerateIdempotencyKeys = true;
});
```

### Sandbox vs. live vs. manual override

Paystack does **not** use different base URLs for test and live traffic — both go to `https://api.paystack.co`, and the environment is determined entirely by which secret key you use (`sk_test_...` vs `sk_live_...`). The SDK follows this: just swap `SecretKey` between environments (e.g. via configuration per deployment slot) and everything else — retries, logging, webhook verification — stays the same.

`PayStackOptions.ResolveEnvironment()` tells you which one you're pointed at, if you need it for logging/telemetry:

```csharp
var env = options.ResolveEnvironment(); // PayStackEnvironment.Sandbox or .Live
```

`BaseUrlOverride` exists purely for testing against a local mock server, a corporate egress proxy, or a contract-testing harness — leave it unset in normal use.

## Error handling

Every resource method returns `Task<ApiResponse<PayStackResponse<T>>>` (from Refit / this SDK, respectively). **Refit is configured to never throw for non-2xx responses** (`RefitSettings.ExceptionFactory` always returns `null` — see `PayStackRefitSettingsFactory`), so you always get a value back and check it explicitly:

```csharp
var response = await payStack.Refunds.CreateAsync(new CreateRefundRequest { Transaction = "T12345" });

if (!response.IsSuccessStatusCode)
{
    // Network-level / HTTP-level failure (4xx/5xx). response.Error has the Refit ApiException details;
    // response.Content may still be populated if Paystack returned a JSON error body alongside the status code.
    logger.LogWarning("Refund request failed: {Status} {Message}", response.StatusCode, response.Content?.Message);
    return;
}

var body = response.Content!;
if (!body.Status)
{
    // HTTP 200 but Paystack reports a logical failure — this happens on some endpoints
    // (e.g. transaction verify/charge always return 200; check `data.status` instead of `body.Status`).
    logger.LogWarning("Paystack reported failure: {Message}", body.Message);
    return;
}

var refund = body.Data!;
```

Rules of thumb baked into every resource client:
- Check `response.IsSuccessStatusCode` first (HTTP-level).
- Then check `response.Content.Status` (Paystack's logical status).
- For `Transactions.VerifyAsync` / `ChargeAuthorizationAsync` / `PartialDebitAsync` / `Charge.*`, Paystack returns HTTP 200 even for a **declined** charge — always inspect `data.status` (`PayStackTransactionStatus` constants) rather than stopping at `body.Status`.
- `response.Error` (a Refit `ApiException`) is populated for transport/HTTP failures; it is **not** thrown, just attached to the response for inspection/logging.

### Distinguishing "Paystack is down" from "the request was rejected"

Transient failures (DNS/connection errors, timeouts, 5xx, 429, and the circuit breaker tripping) are handled by the resilience pipeline (below) and surface as one of two dedicated exception types — these *do* throw, because there's no HTTP response to hand back as an `ApiResponse<T>`:

```csharp
using PayStack.Net.Http; // PayStackCircuitOpenException, PayStackTimeoutException

try
{
    var response = await payStack.Transfers.InitiateAsync(request);
}
catch (PayStackCircuitOpenException)
{
    // Paystack (or the network path to it) has been failing repeatedly; the circuit breaker is
    // short-circuiting new calls for a while rather than piling on more load. Retry later.
}
catch (PayStackTimeoutException)
{
    // No response within the configured timeout — also transient, not a rejection.
}
```

Everything else — validation errors, declined payments, not-found, insufficient balance — comes back as a normal `ApiResponse<T>` with `IsSuccessStatusCode == false` or `Content.Status == false`, never an exception.

## Resilience

Every request goes through a Polly v8 pipeline (`Microsoft.Extensions.Http.Resilience`), configured from `PayStackOptions`:

1. **Per-attempt timeout** (`AttemptTimeout`, default 30s).
2. **Retry** (`MaxRetryAttempts`, default 3) with exponential backoff + jitter, on transport exceptions, timeouts, `408`, `429`, and any `5xx`.
3. **Circuit breaker** (`CircuitBreakerFailureThreshold` / `CircuitBreakerBreakDuration`) — opens after a burst of failures and fails fast (`PayStackCircuitOpenException`) instead of continuing to hammer a struggling endpoint.
4. **Overall timeout** (`TotalTimeout`, default 90s) across all attempts combined.

Set `MaxRetryAttempts = 0` to disable retries entirely (e.g. if you have your own outer retry/outbox layer and don't want double-retries).

## Idempotency keys

Paystack deduplicates retried requests that carry the same `Idempotency-Key` header — critical for anything that moves money, since a network blip + naive retry must never risk a duplicate transfer, refund, or charge.

By default (`PayStackOptions.AutoGenerateIdempotencyKeys = true`), the SDK automatically attaches a fresh key to every POST/PUT request that doesn't already have one. Money-movement methods (`Transfers.InitiateAsync`, `TransferRecipients.CreateAsync`, `Refunds.CreateAsync`, `Transactions.ChargeAuthorizationAsync`, etc.) also expose an explicit `idempotencyKey` parameter so you can supply your own — e.g. derived from your own order id — so a retry from *your* application layer (not just the HTTP layer) is also safe:

```csharp
var idempotencyKey = $"payout-{payoutId}";

var response = await payStack.Transfers.InitiateAsync(new InitiateTransferRequest
{
    Source = "balance",
    Amount = 1000000, // in subunits
    Recipient = recipientCode,
    Reference = idempotencyKey, // Paystack's own dedupe key on the resource; combine with the header for full protection
    Reason = "Vendor payout",
}, idempotencyKey);
```

## Resource areas

`IPayStackClient` exposes one client per Paystack API resource group:

| Property | Covers |
|---|---|
| `Transactions` | Initialize, verify, list, fetch, charge authorization, timeline, totals, export, partial debit |
| `TransactionSplits` | Split a payment between multiple accounts |
| `Charge` | Direct channel charges: bank, USSD, mobile money, QR, EFT, Capitec Pay, OTP/PIN submission |
| `Preauthorization` | Reserve and capture/release funds (South Africa, ZAR) |
| `Customers` | Create/list/fetch/update/validate customers, authorizations, risk actions |
| `DirectDebit` | Trigger activation charges, list mandate authorizations |
| `DedicatedVirtualAccounts` | Create/assign/manage dedicated bank accounts |
| `ApplePay` | Register/list/unregister Apple Pay domains |
| `CapitecPay` | Requery Capitec Pay transactions (**note:** this endpoint authenticates with your **public** key, not the secret key used everywhere else — see the XML doc on `ICapitecPayClient`) |
| `Subaccounts` | Create/list/fetch/update subaccounts for split settlements |
| `Verification` | Resolve/validate bank accounts, resolve card BINs |
| `Miscellaneous` | Banks, countries, states/AVS lookups |
| `Plans` | Billing plans — pricing tiers for subscriptions |
| `Subscriptions` | Create/list/fetch/enable/disable recurring subscriptions, update links |
| `Products` | Create/list/fetch/update products |
| `Storefronts` | Create/manage storefronts, their products and orders |
| `Orders` | Create/list/fetch/validate storefront orders |
| `PaymentPages` | Create/manage hosted payment pages |
| `PaymentRequests` | Create/manage invoices — send, finalize, track totals |
| `Settlements` | Inspect settlements and their transactions |
| `TransferRecipients` | Create/manage payout destinations (bulk supported) |
| `Transfers` | Initiate/finalize/verify payouts (single and bulk) |
| `TransfersControl` | Balance, ledger, OTP requirement management |
| `BulkCharges` | Charge many customers in one batch |
| `Integration` | Account-level integration settings (session timeout) |
| `Disputes` | List/update/resolve chargebacks, submit evidence |
| `Refunds` | Create/retry/list/fetch refunds |
| `Terminal` | Manage POS terminals, send events |
| `VirtualTerminal` | Create/manage virtual terminals |

Every interface method's XML doc links back to the exact Paystack API reference page for that resource.

> The examples below assume the standard `using` block from [Namespaces](#namespaces) (`PayStack.Net`, `PayStack.Net.Models.Requests`, `PayStack.Net.Models.Responses`, `PayStack.Net.Models.Common`) is already in scope.

### Example: subscriptions (recurring billing / pricing tiers)

```csharp
// 1. Create a plan (a pricing tier).
var plan = await payStack.Plans.CreateAsync(new CreatePlanRequest
{
    Name = "Pro Monthly",
    Amount = "1000000", // NGN 10,000.00/month
    Interval = PayStackPlanInterval.Monthly,
});

// 2. Subscribe a customer to it (customer must have a reusable authorization on file already,
//    e.g. from a prior Transactions.InitializeAsync charge).
var subscription = await payStack.Subscriptions.CreateAsync(new CreateSubscriptionRequest
{
    Customer = customerCode,
    Plan = plan.Content!.Data!.PlanCode,
});
```

Paystack's API has no separate "free trial" endpoint — trials are modeled by delaying the first invoice (via the plan's invoice limit / a future subscription start date), which is what `CreateSubscriptionRequest.StartDate` is for; see its XML doc for details.

### Example: refunds

```csharp
var response = await payStack.Refunds.CreateAsync(new CreateRefundRequest
{
    Transaction = "T685409429",
    Amount = 50000, // partial refund, in subunits; omit for a full refund
}, idempotencyKey: $"refund-{orderId}");
```

## Webhooks

Paystack signs every webhook delivery with `x-paystack-signature`: an HMAC-SHA512 of the raw request body, keyed with your secret key, hex-encoded. **Always verify this before trusting the payload** — never process a webhook body based on IP alone or without a valid signature.

### ASP.NET Core

```csharp
using PayStack.Net.Webhooks;
using PayStack.Net.Models.Common;

app.MapPost("/webhooks/paystack", async (HttpRequest request, IPayStackWebhookParser parser, ILogger<Program> logger) =>
{
    request.EnableBuffering();
    var evt = await request.ReadPayStackWebhookEventAsync(parser);

    if (evt is null)
    {
        return Results.Unauthorized(); // missing/invalid signature, or malformed body
    }

    switch (evt.Event)
    {
        case PayStackWebhookEventType.ChargeSuccess:
            var transaction = evt.GetData<TransactionData>();
            // reconcile the order...
            break;

        case PayStackWebhookEventType.TransferFailed:
        case PayStackWebhookEventType.TransferReversed:
            var transfer = evt.GetData<TransferData>();
            // alert ops, retry payout, etc.
            break;

        case PayStackWebhookEventType.SubscriptionDisable:
            var subscription = evt.GetData<SubscriptionData>();
            // downgrade the account...
            break;
    }

    return Results.Ok(); // ack fast — Paystack retries on anything other than 2xx
});
```

`IPayStackWebhookParser` is registered by `AddPayStack` and reads `WebhookSecretKey` (falling back to `SecretKey`) from `PayStackOptions`.

### Outside ASP.NET Core (Azure Functions, worker services, etc.)

Use the static, dependency-free verifier directly:

```csharp
using PayStack.Net.Webhooks;

if (!PayStackWebhookSignatureVerifier.Verify(rawBody, signatureHeader, secretKey))
{
    return; // reject
}

using var doc = JsonDocument.Parse(rawBody);
var eventType = doc.RootElement.GetProperty("event").GetString();
// ...
```

### Event catalog

`PayStackWebhookEventType` has a constant for every documented event (`charge.success`, `transfer.success`, `subscription.create`, `invoice.payment_failed`, `refund.processed`, `charge.dispute.create`, `dedicatedaccount.assign.success`, `customeridentification.failed`, ...). These are modeled as `string` constants rather than a C# `enum` (see [Design notes](#design-notes)) — `PayStackWebhookEvent.Event` is a plain `string`, so unrecognized future event types don't break deserialization; just compare against the constant or fall through to a `default` case.

Each event's `data` shape matches the corresponding resource's response DTO (e.g. `charge.success` → `TransactionData`, `subscription.create` → `SubscriptionData`, `transfer.success` → `TransferData`) — use `evt.GetData<T>()` (throws if the shape doesn't match) or `evt.TryGetData<T>(out var data)` (returns `false` instead).

### Local testing

Paystack cannot deliver webhooks to `localhost`. Use a tunnel (ngrok, VS/VSCode dev tunnels, etc.) and point your Paystack dashboard's webhook URL at the tunnel while developing.

## Design notes

- **Enums as string constants, not C# `enum`s.** Every open-ended API string field (transaction status, event type, payment channel, dispute status, ...) is typed as `string`/`string?` on the DTO, paired with a `public static class PayStack<Thing>` of `const string` members for IntelliSense. A closed C# `enum` would silently fail to deserialize (or worse, throw) the moment Paystack ships a new value — these constants keep the SDK forward-compatible without a breaking release.
- **Money is always `long` (or `string` on requests that document it as a string), never `decimal`/`double`.** Amounts are in the currency's subunit (kobo, cents, pesewas, ...) per Paystack's convention — multiply your display amount by 100 before sending it.
- **Loosely-structured fields** (metadata blobs, provider passthrough objects) are typed as `System.Text.Json.JsonElement?` rather than guessed at, with convenience `GetXObject<T>()` accessors on a few high-traffic ones (e.g. `TransactionData.GetMetadataObject()`).
- **Logging** uses `Microsoft.Extensions.Logging.ILogger<T>` (not a concrete logging framework) so the SDK stays sink-agnostic — wire up Serilog, or any other provider, in your host. Request/response logging never includes headers or bodies, so secret keys, card numbers, and customer PII never reach your log sink; only method, path, status code, and elapsed time are logged (`Information` on success, `Warning` on non-2xx, `Error` with exception on transport failure).
- **No secrets in exceptions or logs.** `PayStackAuthHandler` reads the secret key fresh from `IOptionsMonitor` per request (supporting key rotation without restarting) and never logs it.
- **Packaging.** Release builds always run with `ContinuousIntegrationBuild=true` and an explicit `PathMap` (see `Directory.Build.props`), so a package built from a local clone never embeds the machine's directory layout into the DLL/PDB — only `github.com/Godhanded/PayStack.Net` source links, once the repo has a commit for SourceLink to map against.

## Contributing, building, and publishing

Bug reports, feature requests, and PRs are welcome — see [CONTRIBUTING.md](CONTRIBUTING.md) for project layout, build/test commands, code style, and the release process. Security issues go through [SECURITY.md](SECURITY.md), not a public issue. See the [Code of Conduct](CODE_OF_CONDUCT.md) for community expectations.

## License

[MIT](LICENSE)
