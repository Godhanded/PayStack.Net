using PayStack.Net.Models.Common;
using PayStack.Net.Models.Requests;
using PayStack.Net.Models.Responses;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Plans API: create and manage subscription billing plans (recurring pricing tiers) that
/// customers can be subscribed to via the Subscriptions API.
/// See <see href="https://paystack.com/docs/api/plan/">Paystack API reference — Plans</see>.
/// </summary>
public interface IPlansClient
{
    /// <summary>Creates a new billing plan.</summary>
    /// <param name="request">Name, amount, interval, and optional invoicing configuration.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/plan")]
    Task<ApiResponse<PayStackResponse<PlanData>>> CreateAsync(
        [Body] CreatePlanRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Lists plans on your integration.</summary>
    /// <param name="perPage">Records per page. Defaults to 50.</param>
    /// <param name="page">Page number (offset pagination). Defaults to 1.</param>
    /// <param name="status">Filter by plan status.</param>
    /// <param name="interval">Filter by billing interval. See <see cref="PayStackPlanInterval"/>.</param>
    /// <param name="amount">Filter by exact amount, in subunits.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/plan")]
    Task<ApiResponse<PayStackResponse<List<PlanData>>>> ListAsync(
        [Query] int? perPage = null,
        [Query] int? page = null,
        [Query] string? status = null,
        [Query] string? interval = null,
        [Query] long? amount = null,
        CancellationToken cancellationToken = default);

    /// <summary>Fetches a single plan by its numeric id or plan code.</summary>
    /// <param name="idOrCode">The plan's numeric id or "PLN_..." code.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/plan/{idOrCode}")]
    Task<ApiResponse<PayStackResponse<PlanData>>> FetchAsync(
        string idOrCode,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a plan. Returns no <c>data</c> payload — check <c>message</c> for a summary such as
    /// "Plan updated. 1 subscription(s) affected".
    /// </summary>
    /// <param name="idOrCode">The plan's numeric id or "PLN_..." code.</param>
    /// <param name="request">Full replacement of the plan's name, amount, and interval, plus optional fields.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Put("/plan/{idOrCode}")]
    Task<ApiResponse<PayStackResponse<object>>> UpdateAsync(
        string idOrCode,
        [Body] UpdatePlanRequest request,
        CancellationToken cancellationToken = default);
}
