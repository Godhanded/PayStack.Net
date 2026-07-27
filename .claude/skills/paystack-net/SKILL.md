---
name: paystack-net
description: >-
  Complete API reference and usage patterns for PayStack.Net, the fully typed
  .NET SDK for the Paystack payments API (paystack.com/docs/api) — this repo,
  NuGet package id "PayStackDotNet" (assembly/namespace/repo stay
  "PayStack.Net" — only the package id differs, since "PayStack.Net" was
  already registered by an unrelated package). Covers install, namespaces, the Refit
  ApiResponse<PayStackResponse<T>> non-throwing error pattern, idempotency
  keys, resilience (retry/circuit breaker/timeout), logging, and every one of
  the 29 resource clients: Transactions, Transaction Splits, Charge,
  Preauthorization, Customers, Direct Debit, Dedicated Virtual Accounts,
  Apple Pay, Capitec Pay, Subaccounts, Verification, Miscellaneous, Plans,
  Subscriptions, Products, Storefronts, Orders, Payment Pages, Payment
  Requests, Settlements, Transfer Recipients, Transfers, Transfers Control,
  Bulk Charges, Integration, Disputes, Refunds, Terminal, Virtual Terminal.
  Also covers webhooks (signature verification, typed event payloads, full
  event catalog). Use whenever writing, reviewing, or debugging C# code that
  references the PayStack.Net package, or when integrating with Paystack
  (checkout, subscriptions, payouts/transfers, refunds, webhooks) in a .NET
  project.
---

PayStack.Net is a fully typed .NET SDK for the Paystack API
(paystack.com/docs/api), targeting net8.0/net9.0/net10.0. This skill is the
API reference for writing correct code against it — don't guess method
signatures, field names, or namespaces; they're all below, verified against
the actual source (`src/PayStack.Net` in this repo).

Install: `dotnet add package PayStackDotNet` — the NuGet package id is
`PayStackDotNet`, **not** `PayStack.Net`; that id was already registered by
an unrelated package. The assembly name, root namespace (`PayStack.Net`),
repo name, and every type stay `PayStack.Net` — only the package id differs.

## Namespaces — always add the `using`s you need

| Namespace | Contains |
|---|---|
| `PayStack.Net` | `IPayStackClient` (the facade — one property per resource), `PayStackClient` (`.Create(...)` standalone factory, `DisposablePayStackClient`), `ServiceCollectionExtensions` (`AddPayStack`), `PayStackJsonOptions` |
| `PayStack.Net.Configuration` | `PayStackOptions`, `PayStackEnvironment` |
| `PayStack.Net.Resources` | The 29 Refit resource interfaces (`ITransactionsClient`, `ICustomersClient`, ...) — accessed via `IPayStackClient` properties (`client.Transactions`), rarely referenced by type name directly |
| `PayStack.Net.Models.Requests` | Every request DTO (`InitializeTransactionRequest`, `CreatePlanRequest`, `InitiateTransferRequest`, ...) |
| `PayStack.Net.Models.Responses` | Every response DTO (`TransactionData`, `PlanData`, `TransferData`, `RefundData`, ...) |
| `PayStack.Net.Models.Common` | `PayStackResponse<T>`, `PayStackMeta`, shared sub-objects (`AuthorizationData`, `CustomerSummary`, `TransactionLog`), and every `static class PayStack<Thing>` of `const string` values for enum-shaped fields (`PayStackChannel`, `PayStackTransactionStatus`, `PayStackPlanInterval`, `PayStackTransferStatus`, `PayStackWebhookEventType`, `PayStackRiskAction`, etc. — 30+ of these, one per open-ended string field) |
| `PayStack.Net.Http` | `PayStackAuthHandler`, `IdempotencyKeyHandler`, `PayStackLoggingHandler`, `PayStackCircuitOpenException`, `PayStackTimeoutException` — internal DelegatingHandlers/exception types; the two exception types are the only public members you'll reference directly (in a `catch`) |
| `PayStack.Net.Webhooks` | `IPayStackWebhookParser`, `PayStackWebhookEvent`, `PayStackWebhookSignatureVerifier`, `HttpRequestWebhookExtensions` (the `ReadPayStackWebhookEventAsync` extension on ASP.NET Core's `HttpRequest`) |

Typical `using` block for a controller/handler that calls the API:

```csharp
using PayStack.Net;
using PayStack.Net.Models.Requests;
using PayStack.Net.Models.Responses;
using PayStack.Net.Models.Common;
```

For a webhook receiver, add:

```csharp
using PayStack.Net.Webhooks;
```

## Setup

**DI (ASP.NET Core / generic host)** — preferred:

```csharp
using PayStack.Net;

builder.Services.AddPayStack(options =>
{
    options.SecretKey = builder.Configuration["PayStack:SecretKey"]!; // sk_test_... or sk_live_...
});
```

Or bind straight from an `IConfiguration` section (defaults to section name `"PayStack"`):

```csharp
builder.Services.AddPayStack(builder.Configuration);
// appsettings.json: { "PayStack": { "SecretKey": "sk_test_..." } }
```

Inject `IPayStackClient` like any service. It exposes one property per resource:
`Transactions`, `TransactionSplits`, `Charge`, `Preauthorization`, `Customers`,
`DirectDebit`, `DedicatedVirtualAccounts`, `ApplePay`, `CapitecPay`,
`Subaccounts`, `Verification`, `Miscellaneous`, `Plans`, `Subscriptions`,
`Products`, `Storefronts`, `Orders`, `PaymentPages`, `PaymentRequests`,
`Settlements`, `TransferRecipients`, `Transfers`, `TransfersControl`,
`BulkCharges`, `Integration`, `Disputes`, `Refunds`, `Terminal`, `VirtualTerminal`.

**Standalone (console app, script, Azure Function)** — no DI container required:

```csharp
using var payStack = PayStackClient.Create("sk_test_xxx");
// optional extra config:
using var payStack2 = PayStackClient.Create("sk_test_xxx", options =>
{
    options.MaxRetryAttempts = 5;
});
```

`PayStackClient.Create` returns a `DisposablePayStackClient` (implements
`IPayStackClient` + `IDisposable`, also exposes `.WebhookParser`) backed by a
small internal `ServiceProvider` — dispose it (or the `using`) to release the
HTTP pipeline.

`PayStackOptions` (namespace `PayStack.Net.Configuration`): `SecretKey`
(required), `BaseUrlOverride` (`Uri?`, default `https://api.paystack.co` —
Paystack uses **one** base URL for both environments; environment is
determined entirely by the `sk_test_`/`sk_live_` prefix of `SecretKey`, not
the URL — leave this unset except to point at a proxy/mock), `WebhookSecretKey`
(`string?`, falls back to `SecretKey`), `MaxRetryAttempts` (default 3),
`AttemptTimeout` (default 30s), `TotalTimeout` (default 90s),
`CircuitBreakerFailureThreshold` (default 8), `CircuitBreakerBreakDuration`
(default 30s), `AutoGenerateIdempotencyKeys` (default `true`). Call
`options.ResolveEnvironment()` to get `PayStackEnvironment.Sandbox`/`.Live`.

## Error handling — Refit `ApiResponse<T>`, never throws for expected API failures

Every resource method returns `Task<ApiResponse<PayStackResponse<T>>>`
(`ApiResponse<T>` from `Refit`, `PayStackResponse<T>` from
`PayStack.Net.Models.Common`). Refit is configured
(`PayStackRefitSettingsFactory.ExceptionFactory` always returns `null`) to
**never throw `ApiException` for non-2xx HTTP responses** — always check the
response explicitly, two levels deep:

```csharp
var response = await payStack.Transactions.VerifyAsync(reference);

if (!response.IsSuccessStatusCode)
{
    // HTTP-level failure. response.Error (Refit ApiException) has details;
    // response.Content may still be populated if Paystack sent a JSON body alongside the status.
}

var body = response.Content;
if (body is null || !body.Status)
{
    // Paystack's own logical status is false — body.Message explains why.
}

var transaction = body!.Data!; // the actual TransactionData
```

**Critical gotcha, called out on the relevant methods' XML docs**: Paystack's
charge/verify endpoints return **HTTP 200 even for a declined/failed charge**.
`response.IsSuccessStatusCode` and `body.Status` will both be `true` — you
must additionally check `transaction.Status` (compare against
`PayStackTransactionStatus.Success`, not just any truthy response) before
treating a transaction as paid. This applies to
`Transactions.VerifyAsync`/`ChargeAuthorizationAsync`/`PartialDebitAsync` and
all of `Charge.*`.

### Transient failures (these *do* throw — no HTTP response exists to return)

```csharp
using PayStack.Net.Http;

try
{
    var response = await payStack.Transfers.InitiateAsync(request);
}
catch (PayStackCircuitOpenException)
{
    // Circuit breaker open after repeated failures — fail fast, retry later.
}
catch (PayStackTimeoutException)
{
    // No response within AttemptTimeout/TotalTimeout.
}
```

Everything else (validation errors, declined payments, 404s, insufficient
balance) comes back as a normal `ApiResponse<T>`, never an exception.

## Idempotency keys

`Idempotency-Key` header, auto-attached by `IdempotencyKeyHandler` to every
POST/PUT when `PayStackOptions.AutoGenerateIdempotencyKeys` (default `true`)
and the caller didn't supply one. Money-movement methods additionally expose
an explicit trailing `idempotencyKey` parameter so *your* application-level
retries are also safe, not just the HTTP layer's:

```csharp
await payStack.Transfers.InitiateAsync(request, idempotencyKey: $"payout-{payoutId}");
await payStack.Refunds.CreateAsync(request, idempotencyKey: $"refund-{orderId}");
```

Methods with an `idempotencyKey` parameter: `Transactions.ChargeAuthorizationAsync`,
`Transactions.PartialDebitAsync`, `Preauthorization.CaptureAsync`,
`TransferRecipients.CreateAsync`/`BulkCreateAsync`,
`Transfers.InitiateAsync`/`FinalizeAsync`/`InitiateBulkAsync`,
`BulkCharges.InitiateAsync`, `Refunds.CreateAsync`/`RetryAsync`,
`VirtualTerminal.CreateAsync`. Everywhere else, the header is still
auto-generated, just not exposed as a parameter (pure reads and
idempotent-by-nature updates don't need it).

## Resilience

Polly v8 pipeline (`Microsoft.Extensions.Http.Resilience`) on every request:
per-attempt timeout → retry (exponential backoff + jitter, up to
`MaxRetryAttempts`, on transport exceptions/timeouts/408/429/5xx) → circuit
breaker (`CircuitBreakerFailureThreshold`/`CircuitBreakerBreakDuration`) →
overall `TotalTimeout`. Set `MaxRetryAttempts = 0` to disable retries (e.g.
you have your own outer retry/outbox layer).

## Logging

`Microsoft.Extensions.Logging.ILogger<T>` throughout — no hard dependency on
Serilog/NLog/etc.; wire up whatever sink your host uses.
`PayStackLoggingHandler` logs method+path+status+elapsed at `Debug`
(start)/`Information` (2xx)/`Warning` (non-2xx)/`Error` (transport
exception with the exception object). **Never logs headers or bodies** — no
secret keys, card numbers, or customer PII reach the log sink.
`PayStackWebhookParser` logs a `Warning` on signature/parse failure and
`Information` with the event type on success.

## Resource areas — all 29 clients

Every interface lives in `PayStack.Net.Resources`, every method's XML doc
links to the Paystack API reference page it implements. Reference before
guessing at a method name:

| `IPayStackClient` property | Interface | Key methods |
|---|---|---|
| `Transactions` | `ITransactionsClient` | `InitializeAsync`, `VerifyAsync`, `ListAsync`, `FetchAsync`, `ChargeAuthorizationAsync`, `ViewTimelineAsync`, `GetTotalsAsync`, `ExportAsync`, `PartialDebitAsync` |
| `TransactionSplits` | `ITransactionSplitsClient` | Create/List/Fetch/Update splits, add/update/remove subaccount share |
| `Charge` | `IChargeClient` | `CreateAsync` (direct bank/USSD/mobile-money/QR/EFT/Capitec charge), `SubmitPinAsync`, `SubmitOtpAsync`, `SubmitPhoneAsync`, `SubmitBirthdayAsync`, `SubmitAddressAsync`, `CheckPendingChargeAsync` |
| `Preauthorization` | `IPreauthorizationClient` | `InitializeAsync`, `CaptureAsync`, `ReserveAsync`, `VerifyAsync`, `ReleaseAsync`, `ListAsync` — South Africa/ZAR only |
| `Customers` | `ICustomersClient` | `CreateAsync`, `ListAsync`, `FetchAsync` (→ `CustomerDetailData`, richer than list/create's `CustomerData`), `UpdateAsync`, `ValidateAsync`, `SetRiskActionAsync`, `InitializeAuthorizationAsync`/`VerifyAuthorizationAsync`, `InitializeDirectDebitAsync`, `TriggerDirectDebitActivationChargeAsync`, `FetchMandateAuthorizationsAsync`, `DeactivateAuthorizationAsync` |
| `DirectDebit` | `IDirectDebitClient` | Trigger activation charge, list mandate authorizations |
| `DedicatedVirtualAccounts` | `IDedicatedVirtualAccountsClient` | `CreateAsync`, `AssignAsync`, `ListAsync`, `FetchAsync`, `RequeryAsync`, `DeactivateAsync`, `SplitTransactionAsync`, `RemoveSplitAsync`, `FetchBankProvidersAsync` |
| `ApplePay` | `IApplePayClient` | Register/list/unregister domain |
| `CapitecPay` | `ICapitecPayClient` | **Requery only — authenticates with your PUBLIC key, not `SecretKey`.** Called through the standard DI-registered client, which sends `SecretKey`, so this call will fail auth as wired by default; see the `<remarks>` on `ICapitecPayClient` before using it in production — needs a separate `HttpClient`/key if you actually call it. |
| `Subaccounts` | `ISubaccountsClient` | `CreateAsync`, `ListAsync`, `FetchAsync`, `UpdateAsync` |
| `Verification` | `IVerificationClient` | Resolve account, validate account, resolve card BIN |
| `Miscellaneous` | `IMiscellaneousClient` | List banks, list countries, list states/AVS |
| `Plans` | `IPlansClient` | `CreateAsync`, `ListAsync`, `FetchAsync`, `UpdateAsync` — pricing tiers for `Subscriptions` |
| `Subscriptions` | `ISubscriptionsClient` | `CreateAsync`, `ListAsync`, `FetchAsync`, `EnableAsync`, `DisableAsync`, `GenerateUpdateLinkAsync`, `SendUpdateLinkAsync` — see trial note below |
| `Products` | `IProductsClient` | `CreateAsync`, `ListAsync`, `FetchAsync`, `UpdateAsync` |
| `Storefronts` | `IStorefrontsClient` | Create/List/Fetch/Update/Delete + `VerifySlugAsync`, `FetchOrdersAsync`, `AddProductsAsync`, `ListProductsAsync`, `PublishAsync`, `DuplicateAsync` |
| `Orders` | `IOrdersClient` | Create/List/Fetch, `FetchProductOrdersAsync`, `ValidateAsync` |
| `PaymentPages` | `IPaymentPagesClient` | Create/List/Fetch/Update, `CheckSlugAsync`, `AddProductsAsync` |
| `PaymentRequests` | `IPaymentRequestsClient` | Invoicing — Create/List/Fetch, `VerifyAsync`, `NotifyAsync`, `GetTotalsAsync`, `FinalizeAsync`, `UpdateAsync`, `ArchiveAsync` |
| `Settlements` | `ISettlementsClient` | List settlements, list a settlement's transactions |
| `TransferRecipients` | `ITransferRecipientsClient` | `CreateAsync`, `BulkCreateAsync`, `ListAsync`, `FetchAsync`, `UpdateAsync`, `DeleteAsync` |
| `Transfers` | `ITransfersClient` | `InitiateAsync`, `FinalizeAsync` (OTP confirm), `InitiateBulkAsync`, `ListAsync`, `FetchAsync`, `VerifyAsync` |
| `TransfersControl` | `ITransfersControlClient` | Check balance, fetch balance ledger, resend/disable/finalize-disable/enable transfer OTP |
| `BulkCharges` | `IBulkChargesClient` | `InitiateAsync`, list batches, fetch batch, fetch charges in batch, pause/resume (**pause/resume use `GET`**, per Paystack's docs, not the REST-conventional verb — this is intentional, matches the live API) |
| `Integration` | `IIntegrationClient` | Fetch/update account session timeout |
| `Disputes` | `IDisputesClient` | List/fetch/list-by-transaction/update, add evidence, get upload URL, resolve, export |
| `Refunds` | `IRefundsClient` | `CreateAsync`, `RetryAsync`, `ListAsync`, `FetchAsync` |
| `Terminal` | `ITerminalClient` | Send event, fetch event status, fetch terminal status, List/Fetch/Update, commission, decommission |
| `VirtualTerminal` | `IVirtualTerminalClient` | Create/List/Fetch/Update, Deactivate, assign/unassign destination, add/remove split code |

### Transactions — the entry point for taking a payment

```csharp
var response = await payStack.Transactions.InitializeAsync(new InitializeTransactionRequest
{
    Amount = "500000", // NGN 5,000.00 — Paystack amounts are ALWAYS strings/longs in the currency's SUBUNIT (kobo/cents/pesewas), never decimal
    Email = "customer@example.com",
    Currency = "NGN",
    Channels = [PayStackChannel.Card, PayStackChannel.BankTransfer], // string constants, not a C# enum
    CallbackUrl = "https://example.com/paystack/callback",
});
var checkoutUrl = response.Content!.Data!.AuthorizationUrl; // redirect the customer here

// After redirect/webhook, verify server-side — never trust the redirect alone:
var verify = await payStack.Transactions.VerifyAsync(reference);
if (verify.Content?.Data?.Status == PayStackTransactionStatus.Success) { /* fulfil */ }
```

`TransactionData.Id` is `ulong` (not `long`) — Paystack ids can exceed
`long.MaxValue` for high-volume accounts; this is the one place in the SDK
that deviates from the usual `long` id convention (called out explicitly
because every other resource's numeric id is `long`).

### Subscriptions & Plans — recurring billing / pricing tiers

```csharp
var plan = await payStack.Plans.CreateAsync(new CreatePlanRequest
{
    Name = "Pro Monthly",
    Amount = "1000000", // NGN 10,000.00/month, subunit
    Interval = PayStackPlanInterval.Monthly, // Hourly is update-only, not accepted on create
});

var subscription = await payStack.Subscriptions.CreateAsync(new CreateSubscriptionRequest
{
    Customer = customerCode, // needs a reusable authorization on file already (e.g. from a prior Transactions.InitializeAsync charge)
    Plan = plan.Content!.Data!.PlanCode,
    StartDate = futureIso8601Timestamp, // optional — Paystack has NO dedicated free-trial endpoint; delay the first charge via StartDate (or the plan's invoice_limit/send_invoices) to model a trial
});
```

### Transfers (payouts) & Transfer Recipients

```csharp
var recipient = await payStack.TransferRecipients.CreateAsync(new CreateTransferRecipientRequest
{
    Type = PayStackTransferRecipientType.Nuban,
    Name = "Jane Doe",
    AccountNumber = "0123456789",
    BankCode = "058",
    Currency = "NGN",
});

var transfer = await payStack.Transfers.InitiateAsync(new InitiateTransferRequest
{
    Source = "balance", // only supported value
    Amount = 1_000_000, // long, subunit — note: request Amount here is `long`, NOT a string (differs from Transactions/Charge, which use string amounts — always check the specific DTO)
    Recipient = recipient.Content!.Data!.RecipientCode,
    Reference = idempotencyKey, // Paystack's own dedupe field on the resource, distinct from the Idempotency-Key header — use both for full duplicate-payout protection
    Reason = "Vendor payout",
}, idempotencyKey);
```

### Refunds

```csharp
await payStack.Refunds.CreateAsync(new CreateRefundRequest
{
    Transaction = "T685409429", // reference or numeric id, as a string
    Amount = 50000, // long, subunit; omit for a full refund
}, idempotencyKey: $"refund-{orderId}");
```

### Dedicated Virtual Accounts / Subaccounts / Verification

```csharp
var dva = await payStack.DedicatedVirtualAccounts.CreateAsync(new CreateDedicatedVirtualAccountRequest
{
    Customer = customerCode,
    PreferredBank = "wema-bank",
});

var resolved = await payStack.Verification.ResolveAccountAsync(accountNumber, bankCode);
```

### Marketplace / platform / connected-account style setups

Paystack has no Stripe-Connect-style onboarding-link API. Model a
platform-with-vendors setup as: one `ISubaccountsClient.CreateAsync` per
vendor (their own settlement bank account, KYC happens on Paystack's side,
not through a documented onboarding endpoint), then either pass `Subaccount`
on `InitializeTransactionRequest`/`ChargeAuthorizationRequest` for a simple
platform-fee-then-passthrough split, or create an `ITransactionSplitsClient`
split (percentage/flat across several subaccounts) and apply its
`SplitCode` on the transaction. Payouts to a vendor beyond their automatic
settlement share go through `TransferRecipients`/`Transfers` like any payout.

## Webhooks

Paystack signs every delivery with `x-paystack-signature`: HMAC-SHA512 of the
raw request body, keyed with your secret key, hex-encoded. **Always verify
before trusting the payload.**

### ASP.NET Core

```csharp
using PayStack.Net.Webhooks;
using PayStack.Net.Models.Common;
using PayStack.Net.Models.Responses;

app.MapPost("/webhooks/paystack", async (HttpRequest request, IPayStackWebhookParser parser, ILogger<Program> logger) =>
{
    request.EnableBuffering();
    var evt = await request.ReadPayStackWebhookEventAsync(parser); // null => invalid/missing signature or malformed body

    if (evt is null) return Results.Unauthorized();

    switch (evt.Event) // plain string — compare against PayStackWebhookEventType constants
    {
        case PayStackWebhookEventType.ChargeSuccess:
            var transaction = evt.GetData<TransactionData>(); // throws if shape mismatch; evt.TryGetData<T>(out var d) doesn't
            break;
        case PayStackWebhookEventType.TransferFailed:
        case PayStackWebhookEventType.TransferReversed:
            var transfer = evt.GetData<TransferData>();
            break;
        case PayStackWebhookEventType.SubscriptionDisable:
            var subscription = evt.GetData<SubscriptionData>();
            break;
    }

    return Results.Ok(); // ack fast — anything but 2xx triggers Paystack's retry schedule
});
```

`IPayStackWebhookParser` is DI-registered by `AddPayStack`
(`PayStackWebhookParser`, reads `WebhookSecretKey` ?? `SecretKey`).

### Outside ASP.NET Core

```csharp
using PayStack.Net.Webhooks;

if (!PayStackWebhookSignatureVerifier.Verify(rawBody, signatureHeader, secretKey)) return; // reject
using var doc = System.Text.Json.JsonDocument.Parse(rawBody);
var eventType = doc.RootElement.GetProperty("event").GetString();
```

`PayStackWebhookSignatureVerifier.SignatureHeaderName` = `"x-paystack-signature"`.

### Event catalog (`PayStackWebhookEventType`, namespace `PayStack.Net.Models.Common`)

`ChargeSuccess`, `ChargeDisputeCreate`/`Remind`/`Resolve`,
`CustomerIdentificationFailed`/`Success`, `DedicatedAccountAssignFailed`/`Success`,
`InvoiceCreate`/`PaymentFailed`/`Update`, `PaymentRequestPending`/`Success`,
`RefundFailed`/`Pending`/`Processed`/`Processing`, `SubscriptionCreate`/`Disable`/`ExpiringCards`/`NotRenew`,
`TransferFailed`/`Success`/`Reversed`. Each event's `data` shape matches the
corresponding resource's response DTO (`charge.success` → `TransactionData`,
`subscription.*` → `SubscriptionData`, `transfer.*` → `TransferData`,
`refund.*` → `RefundData`, `charge.dispute.*` → `DisputeData`) — except
`subscription.expiring_cards`, whose `data` is a JSON **array**, not a
single object.

Paystack cannot deliver to `localhost` — use a tunnel (ngrok, dev tunnels)
while developing.

## Design conventions — apply these when extending the SDK

- **Enums are `string` + a `static class` of `const string`, never a C# `enum`.** Every open-ended API field (status, channel, event type, ...) follows this — forward-compatible with new Paystack values without a breaking release. Compare with `==` against the constant, don't switch exhaustively without a `default`.
- **Money is `long`/`long?` in the currency's subunit almost everywhere in responses**, but **check each request DTO individually** — some (Transactions, Charge, Preauthorization) send amount as a documented `string`; others (Transfers) send `long`. Never `decimal`/`double`.
- **Loosely-structured/polymorphic fields** (metadata blobs, fields that are a bare id on create but an expanded object on fetch) are typed `System.Text.Json.JsonElement?`, sometimes paired with a `GetXObject<T>()` convenience method (e.g. `TransactionData.GetMetadataObject()`).
- **`PayStackJsonOptions.Default`** (namespace `PayStack.Net`) is the single shared `JsonSerializerOptions` — camelCase, enum-as-camelCase-string, and a lenient bool converter (`LenientBooleanConverter`/`LenientNullableBooleanConverter` in `PayStack.Net.Http`) that accepts `0`/`1` as well as `true`/`false`, since a few Paystack list endpoints (e.g. Subaccounts) serialize booleans as integers inconsistently with the same field elsewhere.
- **No hard dependency on ASP.NET Core** in the SDK core — `Microsoft.AspNetCore.Http.Abstractions` (a NuGet-installable package, not the shared framework) is used only for the optional `HttpRequestWebhookExtensions` convenience method; everything else works in a plain console app/worker.
- **Packaging**: `Directory.Build.props` forces `ContinuousIntegrationBuild=true` for any Release build (not just CI) and sets an explicit `PathMap` on the repo root, so a locally-run `dotnet pack -c Release` never embeds the machine's local directory path into the DLL/PDB. `Microsoft.SourceLink.GitHub` is also wired up (`RepositoryUrl`/`RepositoryBranch=master` in the csproj) so once the repo has a git remote + commit, symbol source links point at `github.com/<owner>/PayStack.Net` on `master` instead.

## Known open items (from the implementation pass — verify against a live response before depending on these in production)

- `Refund.Transaction`, `TransferData.Recipient`, `PaymentRequestData.Customer`/`Integration`, `BulkChargeItemData.Transaction` are typed `JsonElement?` because the field is a bare id on some endpoints and a full object on others — use `.GetProperty(...)` or the resource's `Get...Object()` helper rather than assuming a shape.
- `CapitecPay` needs a separate public-key-authenticated `HttpClient` to actually work; the DI-registered client sends `SecretKey` like everything else, which will fail against this one endpoint.
- Dispute `UpdateAsync` returns `List<DisputeData>` (a one-element array) rather than a single object, per the documented (unusual) response shape — don't "fix" this to a single object without re-checking the live API.
