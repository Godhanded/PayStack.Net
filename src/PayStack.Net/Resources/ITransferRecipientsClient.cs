using PayStack.Net.Models.Common;
using PayStack.Net.Models.Requests;
using PayStack.Net.Models.Responses;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Transfer Recipients API: create, list, fetch, update, and delete the recipients that
/// transfers are paid out to.
/// See <see href="https://paystack.com/docs/api/transfer-recipient/">Paystack API reference — Transfer Recipients</see>.
/// </summary>
public interface ITransferRecipientsClient
{
    /// <summary>
    /// Creates a new transfer recipient. Creating a recipient with an account number that already
    /// exists on your integration returns the existing record rather than erroring or duplicating it.
    /// </summary>
    /// <param name="request">Recipient type, name, and bank/authorization details.</param>
    /// <param name="idempotencyKey">
    /// Optional idempotency key. When omitted and <c>PayStackOptions.AutoGenerateIdempotencyKeys</c>
    /// is enabled (default), one is generated automatically so a network-retried request can't create a duplicate recipient.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/transferrecipient")]
    Task<ApiResponse<PayStackResponse<TransferRecipientData>>> CreateAsync(
        [Body] CreateTransferRecipientRequest request,
        [Header("Idempotency-Key")] string? idempotencyKey = null,
        CancellationToken cancellationToken = default);

    /// <summary>Creates multiple transfer recipients in a single request.</summary>
    /// <param name="request">The batch of recipients to create.</param>
    /// <param name="idempotencyKey">Optional idempotency key; auto-generated when omitted (see <see cref="CreateAsync"/>).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/transferrecipient/bulk")]
    Task<ApiResponse<PayStackResponse<BulkCreateTransferRecipientData>>> BulkCreateAsync(
        [Body] BulkCreateTransferRecipientRequest request,
        [Header("Idempotency-Key")] string? idempotencyKey = null,
        CancellationToken cancellationToken = default);

    /// <summary>Lists transfer recipients on your integration, most recent first.</summary>
    /// <param name="perPage">Records per page. Defaults to 50.</param>
    /// <param name="page">Page number (offset pagination). Defaults to 1.</param>
    /// <param name="from">Filter to recipients created on or after this timestamp.</param>
    /// <param name="to">Filter to recipients created on or before this timestamp.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/transferrecipient")]
    Task<ApiResponse<PayStackResponse<List<TransferRecipientData>>>> ListAsync(
        [Query] int? perPage = null,
        [Query] int? page = null,
        [Query] DateTimeOffset? from = null,
        [Query] DateTimeOffset? to = null,
        CancellationToken cancellationToken = default);

    /// <summary>Fetches a single transfer recipient by its numeric id or recipient code.</summary>
    /// <param name="idOrCode">The recipient's numeric id or "RCP_xxx" code.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/transferrecipient/{idOrCode}")]
    Task<ApiResponse<PayStackResponse<TransferRecipientData>>> FetchAsync(
        string idOrCode,
        CancellationToken cancellationToken = default);

    /// <summary>Updates a transfer recipient's name and/or email.</summary>
    /// <param name="idOrCode">The recipient's numeric id or "RCP_xxx" code.</param>
    /// <param name="request">The fields to update.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Put("/transferrecipient/{idOrCode}")]
    Task<ApiResponse<PayStackResponse<TransferRecipientData>>> UpdateAsync(
        string idOrCode,
        [Body] UpdateTransferRecipientRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Deactivates a transfer recipient. This is a soft delete — the recipient's <c>is_deleted</c>/<c>active</c> flags flip rather than the record being removed.</summary>
    /// <param name="idOrCode">The recipient's numeric id or "RCP_xxx" code.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Delete("/transferrecipient/{idOrCode}")]
    Task<ApiResponse<PayStackResponse<object?>>> DeleteAsync(
        string idOrCode,
        CancellationToken cancellationToken = default);
}
