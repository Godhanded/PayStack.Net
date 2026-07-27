using PayStack.Net.Models.Common;
using PayStack.Net.Models.Requests;
using PayStack.Net.Models.Responses;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Transaction Splits API: share a single payment across multiple subaccounts automatically.
/// See <see href="https://paystack.com/docs/api/split/">Paystack API reference — Transaction Splits</see>.
/// </summary>
public interface ITransactionSplitsClient
{
    /// <summary>Creates a new transaction split.</summary>
    /// <param name="request">Name, type, currency, participating subaccounts, and fee bearer configuration.</param>
    /// <param name="idempotencyKey">
    /// Optional idempotency key. When omitted and <c>PayStackOptions.AutoGenerateIdempotencyKeys</c>
    /// is enabled (default), one is generated automatically so a network-retried request can't create a duplicate split.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/split")]
    Task<ApiResponse<PayStackResponse<SplitData>>> CreateAsync(
        [Body] CreateSplitRequest request,
        [Header("Idempotency-Key")] string? idempotencyKey = null,
        CancellationToken cancellationToken = default);

    /// <summary>Lists transaction splits on your integration.</summary>
    /// <param name="name">Filter by split name.</param>
    /// <param name="active">Filter by active status.</param>
    /// <param name="sortBy">Field to sort by.</param>
    /// <param name="perPage">Records per page.</param>
    /// <param name="page">Page number.</param>
    /// <param name="from">Filter to splits created on or after this timestamp.</param>
    /// <param name="to">Filter to splits created on or before this timestamp.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/split")]
    Task<ApiResponse<PayStackResponse<List<SplitData>>>> ListAsync(
        [Query] string? name = null,
        [Query] bool? active = null,
        [AliasAs("sort_by")][Query] string? sortBy = null,
        [Query] int? perPage = null,
        [Query] int? page = null,
        [Query] DateTimeOffset? from = null,
        [Query] DateTimeOffset? to = null,
        CancellationToken cancellationToken = default);

    /// <summary>Fetches a single transaction split by its Paystack id.</summary>
    /// <param name="id">The split's numeric id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/split/{id}")]
    Task<ApiResponse<PayStackResponse<SplitData>>> FetchAsync(
        long id,
        CancellationToken cancellationToken = default);

    /// <summary>Updates a transaction split's name, active status, or fee bearer configuration.</summary>
    /// <param name="id">The split's numeric id.</param>
    /// <param name="request">Fields to update.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Put("/split/{id}")]
    Task<ApiResponse<PayStackResponse<SplitData>>> UpdateAsync(
        long id,
        [Body] UpdateSplitRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Adds a subaccount to a split, or updates its share if it's already a member.</summary>
    /// <param name="id">The split's numeric id.</param>
    /// <param name="request">The subaccount code and its share.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/split/{id}/subaccount/add")]
    Task<ApiResponse<PayStackResponse<SplitData>>> AddOrUpdateSubaccountAsync(
        long id,
        [Body] SplitSubaccountShare request,
        CancellationToken cancellationToken = default);

    /// <summary>Removes a subaccount from a split. Returns no <c>data</c> payload on success.</summary>
    /// <param name="id">The split's numeric id.</param>
    /// <param name="request">The subaccount code to remove.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/split/{id}/subaccount/remove")]
    Task<ApiResponse<PayStackResponse<object?>>> RemoveSubaccountAsync(
        long id,
        [Body] RemoveSplitSubaccountRequest request,
        CancellationToken cancellationToken = default);
}
