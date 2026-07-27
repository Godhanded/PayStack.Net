using PayStack.Net.Models.Common;
using PayStack.Net.Models.Requests;
using PayStack.Net.Models.Responses;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Bulk Charges API: charge a list of stored authorizations in a single batch, and manage/inspect
/// that batch's progress.
/// See <see href="https://paystack.com/docs/api/bulk-charge/">Paystack API reference — Bulk Charges</see>.
/// </summary>
public interface IBulkChargesClient
{
    /// <summary>
    /// Initiates a batch of charges against previously stored authorization codes. The request body
    /// is a raw JSON array, not wrapped in an object.
    /// </summary>
    /// <param name="charges">The charges to run in this batch.</param>
    /// <param name="idempotencyKey">
    /// Optional idempotency key. When omitted and <c>PayStackOptions.AutoGenerateIdempotencyKeys</c>
    /// is enabled (default), one is generated automatically so a network-retried request can't double-charge the batch.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/bulkcharge")]
    Task<ApiResponse<PayStackResponse<BulkChargeBatchData>>> InitiateAsync(
        [Body] List<InitiateBulkChargeRequestItem> charges,
        [Header("Idempotency-Key")] string? idempotencyKey = null,
        CancellationToken cancellationToken = default);

    /// <summary>Lists bulk charge batches on your integration, most recent first.</summary>
    /// <param name="perPage">Records per page. Defaults to 50.</param>
    /// <param name="page">Page number (offset pagination). Defaults to 1.</param>
    /// <param name="from">Filter to batches created on or after this timestamp.</param>
    /// <param name="to">Filter to batches created on or before this timestamp.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/bulkcharge")]
    Task<ApiResponse<PayStackResponse<List<BulkChargeBatchData>>>> ListBatchesAsync(
        [Query] int? perPage = null,
        [Query] int? page = null,
        [Query] DateTimeOffset? from = null,
        [Query] DateTimeOffset? to = null,
        CancellationToken cancellationToken = default);

    /// <summary>Fetches a single bulk charge batch by its numeric id or batch code.</summary>
    /// <param name="idOrCode">The batch's numeric id or "BCH_xxx" code.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/bulkcharge/{idOrCode}")]
    Task<ApiResponse<PayStackResponse<BulkChargeBatchData>>> FetchBatchAsync(
        string idOrCode,
        CancellationToken cancellationToken = default);

    /// <summary>Lists the individual charges within a batch.</summary>
    /// <param name="idOrCode">The batch's numeric id or "BCH_xxx" code.</param>
    /// <param name="status">Filter by charge status. See <see cref="PayStackBulkChargeItemStatus"/>.</param>
    /// <param name="perPage">Records per page. Defaults to 50.</param>
    /// <param name="page">Page number (offset pagination). Defaults to 1.</param>
    /// <param name="from">Filter to charges created on or after this timestamp.</param>
    /// <param name="to">Filter to charges created on or before this timestamp.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/bulkcharge/{idOrCode}/charges")]
    Task<ApiResponse<PayStackResponse<List<BulkChargeItemData>>>> FetchChargesInBatchAsync(
        string idOrCode,
        [Query] string? status = null,
        [Query] int? perPage = null,
        [Query] int? page = null,
        [Query] DateTimeOffset? from = null,
        [Query] DateTimeOffset? to = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Pauses processing of a batch. Note: Paystack documents this as a <c>GET</c> request despite the
    /// mutating effect — this method mirrors that rather than using <c>POST</c>.
    /// </summary>
    /// <param name="batchCode">The batch code ("BCH_xxx") to pause.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/bulkcharge/pause/{batchCode}")]
    Task<ApiResponse<PayStackResponse<object?>>> PauseAsync(
        string batchCode,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Resumes processing of a paused batch. Note: Paystack documents this as a <c>GET</c> request
    /// despite the mutating effect — this method mirrors that rather than using <c>POST</c>.
    /// </summary>
    /// <param name="batchCode">The batch code ("BCH_xxx") to resume.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/bulkcharge/resume/{batchCode}")]
    Task<ApiResponse<PayStackResponse<object?>>> ResumeAsync(
        string batchCode,
        CancellationToken cancellationToken = default);
}
