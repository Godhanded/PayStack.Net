using PayStack.Net.Models.Common;
using PayStack.Net.Models.Requests;
using PayStack.Net.Models.Responses;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Transactions API: initialize, verify, list, fetch, and charge payments.
/// See <see href="https://paystack.com/docs/api/transaction/">Paystack API reference — Transactions</see>.
/// </summary>
public interface ITransactionsClient
{
    /// <summary>
    /// Initializes a new transaction and returns a checkout URL to redirect the customer to.
    /// This is the standard entry point for a hosted checkout payment.
    /// </summary>
    /// <param name="request">Amount, customer email, and optional checkout configuration.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/transaction/initialize")]
    Task<ApiResponse<PayStackResponse<InitializeTransactionData>>> InitializeAsync(
        [Body] InitializeTransactionRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Confirms the final status of a transaction by its reference. Always call this from your
    /// server after a redirect/webhook — never trust the client-side redirect alone, since it can
    /// be spoofed. Returns HTTP 200 even when the transaction failed; check <c>data.status</c>.
    /// </summary>
    /// <param name="reference">The transaction reference to verify.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/transaction/verify/{reference}")]
    Task<ApiResponse<PayStackResponse<TransactionData>>> VerifyAsync(
        string reference,
        CancellationToken cancellationToken = default);

    /// <summary>Lists transactions on your integration, most recent first.</summary>
    /// <param name="perPage">Records per page. Defaults to 50.</param>
    /// <param name="page">Page number (offset pagination). Defaults to 1.</param>
    /// <param name="customer">Filter by Paystack customer id.</param>
    /// <param name="status">Filter by status: "failed", "success", or "abandoned". See <see cref="PayStackTransactionStatus"/>.</param>
    /// <param name="from">Filter to transactions on or after this timestamp.</param>
    /// <param name="to">Filter to transactions on or before this timestamp.</param>
    /// <param name="amount">Filter by exact amount, in subunits.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/transaction")]
    Task<ApiResponse<PayStackResponse<List<TransactionData>>>> ListAsync(
        [Query] int? perPage = null,
        [Query] int? page = null,
        [Query] long? customer = null,
        [Query] string? status = null,
        [Query] DateTimeOffset? from = null,
        [Query] DateTimeOffset? to = null,
        [Query] long? amount = null,
        CancellationToken cancellationToken = default);

    /// <summary>Fetches a single transaction by its Paystack id.</summary>
    /// <param name="id">The transaction's numeric id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/transaction/{id}")]
    Task<ApiResponse<PayStackResponse<TransactionData>>> FetchAsync(
        ulong id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Charges a customer using a previously stored, reusable authorization code — no customer
    /// interaction required. Useful for recurring/off-session charges outside the Subscriptions API.
    /// </summary>
    /// <param name="request">Amount, customer email, and the authorization code to charge.</param>
    /// <param name="idempotencyKey">
    /// Optional idempotency key. When omitted and <c>PayStackOptions.AutoGenerateIdempotencyKeys</c>
    /// is enabled (default), one is generated automatically so a network-retried request can't double-charge.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/transaction/charge_authorization")]
    Task<ApiResponse<PayStackResponse<TransactionData>>> ChargeAuthorizationAsync(
        [Body] ChargeAuthorizationRequest request,
        [Header("Idempotency-Key")] string? idempotencyKey = null,
        CancellationToken cancellationToken = default);

    /// <summary>Retrieves the step-by-step attempt log for a transaction.</summary>
    /// <param name="idOrReference">The transaction's numeric id or its reference string.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/transaction/timeline/{idOrReference}")]
    Task<ApiResponse<PayStackResponse<TransactionLog>>> ViewTimelineAsync(
        string idOrReference,
        CancellationToken cancellationToken = default);

    /// <summary>Returns aggregate transaction volume/count totals for your integration.</summary>
    /// <param name="perPage">Records per page for the underlying aggregation window.</param>
    /// <param name="page">Page number.</param>
    /// <param name="from">Only include transactions on or after this timestamp.</param>
    /// <param name="to">Only include transactions on or before this timestamp.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/transaction/totals")]
    Task<ApiResponse<PayStackResponse<TransactionTotalsData>>> GetTotalsAsync(
        [Query] int? perPage = null,
        [Query] int? page = null,
        [Query] DateTimeOffset? from = null,
        [Query] DateTimeOffset? to = null,
        CancellationToken cancellationToken = default);

    /// <summary>Requests a CSV export of transactions matching the given filters, returned as a signed, time-limited download URL.</summary>
    /// <param name="perPage">Records per page.</param>
    /// <param name="page">Page number.</param>
    /// <param name="from">Only include transactions on or after this timestamp.</param>
    /// <param name="to">Only include transactions on or before this timestamp.</param>
    /// <param name="customer">Filter by Paystack customer id.</param>
    /// <param name="status">Filter by status.</param>
    /// <param name="currency">Filter by ISO 4217 currency code.</param>
    /// <param name="amount">Filter by exact amount, in subunits.</param>
    /// <param name="settled">Filter by settlement status.</param>
    /// <param name="settlement">Filter by settlement id.</param>
    /// <param name="paymentPage">Filter by payment page id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/transaction/export")]
    Task<ApiResponse<PayStackResponse<TransactionExportData>>> ExportAsync(
        [Query] int? perPage = null,
        [Query] int? page = null,
        [Query] DateTimeOffset? from = null,
        [Query] DateTimeOffset? to = null,
        [Query] long? customer = null,
        [Query] string? status = null,
        [Query] string? currency = null,
        [Query] long? amount = null,
        [Query] bool? settled = null,
        [Query] long? settlement = null,
        [Query] long? paymentPage = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Attempts to charge as much of the requested amount as the customer's balance allows
    /// (NGN/GHS only). Commonly used for wallet-style partial payments.
    /// </summary>
    /// <param name="request">Authorization code, currency, and amount to attempt.</param>
    /// <param name="idempotencyKey">Optional idempotency key; auto-generated when omitted (see <see cref="ChargeAuthorizationAsync"/>).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/transaction/partial_debit")]
    Task<ApiResponse<PayStackResponse<TransactionData>>> PartialDebitAsync(
        [Body] PartialDebitRequest request,
        [Header("Idempotency-Key")] string? idempotencyKey = null,
        CancellationToken cancellationToken = default);
}
