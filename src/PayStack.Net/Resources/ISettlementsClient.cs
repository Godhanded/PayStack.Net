using PayStack.Net.Models.Common;
using PayStack.Net.Models.Responses;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Settlements API: view payouts made to your settlement accounts and the transactions behind them.
/// See <see href="https://paystack.com/docs/api/settlement/">Paystack API reference — Settlements</see>.
/// </summary>
public interface ISettlementsClient
{
    /// <summary>Lists settlements made to your settlement accounts, most recent first.</summary>
    /// <param name="perPage">Records per page. Defaults to 50.</param>
    /// <param name="page">Page number (offset pagination). Defaults to 1.</param>
    /// <param name="status">Filter by status: "success", "processing", "pending", or "failed". See <see cref="PayStackSettlementStatus"/>.</param>
    /// <param name="subaccount">Filter by subaccount id, or "none" to only include settlements to your main account.</param>
    /// <param name="from">Filter to settlements on or after this timestamp.</param>
    /// <param name="to">Filter to settlements on or before this timestamp.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/settlement")]
    Task<ApiResponse<PayStackResponse<List<SettlementData>>>> ListAsync(
        [Query] int? perPage = null,
        [Query] int? page = null,
        [Query] string? status = null,
        [Query] string? subaccount = null,
        [Query] DateTimeOffset? from = null,
        [Query] DateTimeOffset? to = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists the transactions that make up a settlement. Note: Paystack's response meta for this
    /// endpoint also includes a <c>total_volume</c> field alongside the usual pagination fields;
    /// that extra field is not currently modeled on <see cref="PayStackMeta"/> and will be dropped
    /// during deserialization.
    /// </summary>
    /// <param name="id">The settlement's numeric id.</param>
    /// <param name="perPage">Records per page. Defaults to 50.</param>
    /// <param name="page">Page number (offset pagination). Defaults to 1.</param>
    /// <param name="from">Filter to transactions on or after this timestamp.</param>
    /// <param name="to">Filter to transactions on or before this timestamp.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/settlement/{id}/transactions")]
    Task<ApiResponse<PayStackResponse<List<TransactionData>>>> ListTransactionsAsync(
        long id,
        [Query] int? perPage = null,
        [Query] int? page = null,
        [Query] DateTimeOffset? from = null,
        [Query] DateTimeOffset? to = null,
        CancellationToken cancellationToken = default);
}
