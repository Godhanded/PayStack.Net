using PayStack.Net.Models.Common;
using PayStack.Net.Models.Requests;
using PayStack.Net.Models.Responses;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Transfers API: initiate, finalize, bulk-initiate, list, fetch, and verify payouts from your
/// Paystack balance. These endpoints move real money — see the idempotency notes on each mutating method.
/// See <see href="https://paystack.com/docs/api/transfer/">Paystack API reference — Transfers</see>.
/// </summary>
public interface ITransfersClient
{
    /// <summary>
    /// Initiates a transfer to a recipient. Status is "pending" when your integration has the
    /// Transfers OTP requirement disabled, or "otp" when an OTP must be submitted via
    /// <see cref="FinalizeAsync"/> to complete it.
    /// </summary>
    /// <param name="request">Source, amount, recipient code, and a unique reference.</param>
    /// <param name="idempotencyKey">
    /// Optional idempotency key. When omitted and <c>PayStackOptions.AutoGenerateIdempotencyKeys</c>
    /// is enabled (default), one is generated automatically so a network-retried request can't trigger a duplicate payout.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/transfer")]
    Task<ApiResponse<PayStackResponse<TransferData>>> InitiateAsync(
        [Body] InitiateTransferRequest request,
        [Header("Idempotency-Key")] string? idempotencyKey = null,
        CancellationToken cancellationToken = default);

    /// <summary>Completes a transfer that is awaiting OTP confirmation.</summary>
    /// <param name="request">The transfer code and the OTP the recipient/account holder received.</param>
    /// <param name="idempotencyKey">Optional idempotency key; auto-generated when omitted (see <see cref="InitiateAsync"/>).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/transfer/finalize_transfer")]
    Task<ApiResponse<PayStackResponse<TransferData>>> FinalizeAsync(
        [Body] FinalizeTransferRequest request,
        [Header("Idempotency-Key")] string? idempotencyKey = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Initiates multiple transfers in a single request. Requires the Transfers OTP requirement to
    /// be disabled on your integration, since bulk transfers cannot be OTP-confirmed individually.
    /// </summary>
    /// <param name="request">Source, currency, and the list of transfers to initiate.</param>
    /// <param name="idempotencyKey">Optional idempotency key; auto-generated when omitted (see <see cref="InitiateAsync"/>).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/transfer/bulk")]
    Task<ApiResponse<PayStackResponse<List<BulkTransferResultData>>>> InitiateBulkAsync(
        [Body] InitiateBulkTransferRequest request,
        [Header("Idempotency-Key")] string? idempotencyKey = null,
        CancellationToken cancellationToken = default);

    /// <summary>Lists transfers on your integration, most recent first.</summary>
    /// <param name="perPage">Records per page. Defaults to 50.</param>
    /// <param name="page">Page number (offset pagination). Defaults to 1.</param>
    /// <param name="recipient">Filter by recipient's numeric id.</param>
    /// <param name="from">Filter to transfers created on or after this timestamp.</param>
    /// <param name="to">Filter to transfers created on or before this timestamp.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/transfer")]
    Task<ApiResponse<PayStackResponse<List<TransferData>>>> ListAsync(
        [Query] int? perPage = null,
        [Query] int? page = null,
        [Query] long? recipient = null,
        [Query] DateTimeOffset? from = null,
        [Query] DateTimeOffset? to = null,
        CancellationToken cancellationToken = default);

    /// <summary>Fetches a single transfer by its numeric id or transfer code.</summary>
    /// <param name="idOrCode">The transfer's numeric id or "TRF_xxx" code.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/transfer/{idOrCode}")]
    Task<ApiResponse<PayStackResponse<TransferData>>> FetchAsync(
        string idOrCode,
        CancellationToken cancellationToken = default);

    /// <summary>Verifies the status of a transfer by its reference.</summary>
    /// <param name="reference">The transfer reference to verify.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/transfer/verify/{reference}")]
    Task<ApiResponse<PayStackResponse<TransferData>>> VerifyAsync(
        string reference,
        CancellationToken cancellationToken = default);
}
