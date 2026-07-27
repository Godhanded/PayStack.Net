using PayStack.Net.Models.Common;
using PayStack.Net.Models.Requests;
using PayStack.Net.Models.Responses;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Refunds API: reverse successful transactions, in whole or in part.
/// See <see href="https://paystack.com/docs/api/refund/">Paystack API reference — Refunds</see>.
/// </summary>
public interface IRefundsClient
{
    /// <summary>
    /// Initiates a refund for a transaction. This moves money back to the customer, so treat it as
    /// a financial write, not an idempotent update.
    /// </summary>
    /// <param name="request">The transaction to refund, and optional partial amount/notes.</param>
    /// <param name="idempotencyKey">
    /// Optional idempotency key. When omitted and <c>PayStackOptions.AutoGenerateIdempotencyKeys</c>
    /// is enabled (default), one is generated automatically so a network-retried request can't double-refund.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/refund")]
    Task<ApiResponse<PayStackResponse<RefundData>>> CreateAsync(
        [Body] CreateRefundRequest request,
        [Header("Idempotency-Key")] string? idempotencyKey = null,
        CancellationToken cancellationToken = default);

    /// <summary>Retries a previously failed refund, supplying bank account details to pay out to directly.</summary>
    /// <param name="id">The refund's numeric id.</param>
    /// <param name="request">The bank account details to retry the refund with.</param>
    /// <param name="idempotencyKey">Optional idempotency key; auto-generated when omitted (see <see cref="CreateAsync"/>).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/refund/retry_with_customer_details/{id}")]
    Task<ApiResponse<PayStackResponse<RefundData>>> RetryAsync(
        long id,
        [Body] RetryRefundRequest request,
        [Header("Idempotency-Key")] string? idempotencyKey = null,
        CancellationToken cancellationToken = default);

    /// <summary>Lists refunds on your integration.</summary>
    /// <param name="transaction">Filter by transaction reference or id.</param>
    /// <param name="currency">Filter by ISO 4217 currency code.</param>
    /// <param name="from">Only include refunds on or after this date.</param>
    /// <param name="to">Only include refunds on or before this date.</param>
    /// <param name="perPage">Records per page. Defaults to 50.</param>
    /// <param name="page">Page number. Defaults to 1.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/refund")]
    Task<ApiResponse<PayStackResponse<List<RefundData>>>> ListAsync(
        [Query] string? transaction = null,
        [Query] string? currency = null,
        [Query] DateTimeOffset? from = null,
        [Query] DateTimeOffset? to = null,
        [Query] int? perPage = null,
        [Query] int? page = null,
        CancellationToken cancellationToken = default);

    /// <summary>Fetches a single refund by its numeric id.</summary>
    /// <param name="id">The refund's numeric id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/refund/{id}")]
    Task<ApiResponse<PayStackResponse<RefundData>>> FetchAsync(
        long id,
        CancellationToken cancellationToken = default);
}
