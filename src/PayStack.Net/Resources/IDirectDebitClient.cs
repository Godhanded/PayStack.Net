using PayStack.Net.Models.Common;
using PayStack.Net.Models.Requests;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Direct Debit API: integration-wide operations on direct debit mandates, complementing the
/// per-customer direct debit endpoints on <see cref="ICustomersClient"/>.
/// See <see href="https://paystack.com/docs/api/directdebit/">Paystack API reference — Direct Debit</see>.
/// </summary>
public interface IDirectDebitClient
{
    /// <summary>Retries the activation charge for multiple customers with pending mandate authorizations at once.</summary>
    /// <param name="request">The ids of the customers to retry.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Put("/directdebit/activation-charge")]
    Task<ApiResponse<PayStackResponse<object?>>> TriggerActivationChargeAsync(
        [Body] TriggerActivationChargeRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Lists direct debit mandate authorizations across your integration, cursor-paginated.</summary>
    /// <param name="cursor">Pagination cursor from a previous response's <c>meta.next</c>.</param>
    /// <param name="status">Filter by mandate status. See <see cref="PayStackMandateStatus"/>.</param>
    /// <param name="perPage">Records per page.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/directdebit/mandate-authorizations")]
    Task<ApiResponse<PayStackResponse<List<MandateAuthorization>>>> ListMandateAuthorizationsAsync(
        [Query] string? cursor = null,
        [Query] string? status = null,
        [AliasAs("per_page")][Query] int? perPage = null,
        CancellationToken cancellationToken = default);
}
