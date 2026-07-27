using PayStack.Net.Models.Common;
using PayStack.Net.Models.Requests;
using PayStack.Net.Models.Responses;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Disputes API: manage chargebacks raised against your transactions — list, fetch, submit
/// evidence, and resolve.
/// See <see href="https://paystack.com/docs/api/dispute/">Paystack API reference — Disputes</see>.
/// </summary>
public interface IDisputesClient
{
    /// <summary>Lists disputes filed against your transactions within a date range.</summary>
    /// <param name="from">Only include disputes on or after this date. Required.</param>
    /// <param name="to">Only include disputes on or before this date. Required.</param>
    /// <param name="perPage">Records per page. Defaults to 50.</param>
    /// <param name="page">Page number. Defaults to 1.</param>
    /// <param name="transaction">Filter by transaction id.</param>
    /// <param name="status">Filter by status. See <see cref="PayStackDisputeStatus"/> for known values.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/dispute")]
    Task<ApiResponse<PayStackResponse<List<DisputeData>>>> ListAsync(
        [Query] DateTimeOffset from,
        [Query] DateTimeOffset to,
        [Query] int? perPage = null,
        [Query] int? page = null,
        [Query] string? transaction = null,
        [Query] string? status = null,
        CancellationToken cancellationToken = default);

    /// <summary>Fetches a single dispute by its id.</summary>
    /// <param name="id">The dispute's id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/dispute/{id}")]
    Task<ApiResponse<PayStackResponse<DisputeData>>> FetchAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>Lists all disputes filed against a specific transaction.</summary>
    /// <param name="id">The transaction's id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/dispute/transaction/{id}")]
    Task<ApiResponse<PayStackResponse<List<DisputeData>>>> ListTransactionDisputesAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a dispute's refund amount and/or evidence filename.
    /// Note: Paystack wraps the single updated dispute in a one-element array here.
    /// </summary>
    /// <param name="id">The dispute's id.</param>
    /// <param name="request">The refund amount and optional uploaded filename.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Put("/dispute/{id}")]
    Task<ApiResponse<PayStackResponse<List<DisputeData>>>> UpdateAsync(
        string id,
        [Body] UpdateDisputeRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Submits evidence for a dispute, e.g. proof of delivery/service.</summary>
    /// <param name="id">The dispute's id.</param>
    /// <param name="request">Customer contact details and service/delivery details.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/dispute/{id}/evidence")]
    Task<ApiResponse<PayStackResponse<DisputeEvidenceData>>> AddEvidenceAsync(
        string id,
        [Body] AddDisputeEvidenceRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Requests a pre-signed URL to upload a supporting evidence file to.</summary>
    /// <param name="id">The dispute's id.</param>
    /// <param name="uploadFilename">The filename (with extension) you intend to upload. Required.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/dispute/{id}/upload_url")]
    Task<ApiResponse<PayStackResponse<DisputeUploadUrlData>>> GetUploadUrlAsync(
        string id,
        [Query] string uploadFilename,
        CancellationToken cancellationToken = default);

    /// <summary>Resolves a dispute as accepted or declined by the merchant.</summary>
    /// <param name="id">The dispute's id.</param>
    /// <param name="request">Resolution outcome, message, refund amount, and evidence.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Put("/dispute/{id}/resolve")]
    Task<ApiResponse<PayStackResponse<DisputeData>>> ResolveAsync(
        string id,
        [Body] ResolveDisputeRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Requests a CSV export of disputes matching the given filters, returned as a signed, time-limited download URL.</summary>
    /// <param name="from">Only include disputes on or after this date. Required.</param>
    /// <param name="to">Only include disputes on or before this date. Required.</param>
    /// <param name="perPage">Records per page.</param>
    /// <param name="page">Page number.</param>
    /// <param name="transaction">Filter by transaction id.</param>
    /// <param name="status">Filter by status. See <see cref="PayStackDisputeStatus"/> for known values.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/dispute/export")]
    Task<ApiResponse<PayStackResponse<DisputeExportData>>> ExportAsync(
        [Query] DateTimeOffset from,
        [Query] DateTimeOffset to,
        [Query] int? perPage = null,
        [Query] int? page = null,
        [Query] string? transaction = null,
        [Query] string? status = null,
        CancellationToken cancellationToken = default);
}
