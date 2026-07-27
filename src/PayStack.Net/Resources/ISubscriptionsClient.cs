using PayStack.Net.Models.Common;
using PayStack.Net.Models.Requests;
using PayStack.Net.Models.Responses;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Subscriptions API: enroll customers into recurring billing plans, and manage those
/// subscriptions. Paystack has no dedicated "free trial" endpoint — a trial period is modeled by
/// delaying the first charge, either via <see cref="CreateSubscriptionRequest.StartDate"/> on create
/// or via the plan's invoicing configuration.
/// See <see href="https://paystack.com/docs/api/subscription/">Paystack API reference — Subscriptions</see>.
/// </summary>
public interface ISubscriptionsClient
{
    /// <summary>Subscribes a customer to a plan.</summary>
    /// <param name="request">Customer, plan, and optional authorization code / start date.</param>
    /// <param name="idempotencyKey">
    /// Optional idempotency key. When omitted and <c>PayStackOptions.AutoGenerateIdempotencyKeys</c>
    /// is enabled (default), one is generated automatically so a network-retried request can't create
    /// a duplicate recurring subscription.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/subscription")]
    Task<ApiResponse<PayStackResponse<SubscriptionData>>> CreateAsync(
        [Body] CreateSubscriptionRequest request,
        [Header("Idempotency-Key")] string? idempotencyKey = null,
        CancellationToken cancellationToken = default);

    /// <summary>Lists subscriptions on your integration.</summary>
    /// <param name="perPage">Records per page. Defaults to 50.</param>
    /// <param name="page">Page number (offset pagination). Defaults to 1.</param>
    /// <param name="customer">Filter by Paystack customer id.</param>
    /// <param name="plan">Filter by Paystack plan id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/subscription")]
    Task<ApiResponse<PayStackResponse<List<SubscriptionData>>>> ListAsync(
        [Query] int? perPage = null,
        [Query] int? page = null,
        [Query] long? customer = null,
        [Query] long? plan = null,
        CancellationToken cancellationToken = default);

    /// <summary>Fetches a single subscription by its numeric id or subscription code, including its invoice history.</summary>
    /// <param name="idOrCode">The subscription's numeric id or "SUB_..." code.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/subscription/{idOrCode}")]
    Task<ApiResponse<PayStackResponse<SubscriptionData>>> FetchAsync(
        string idOrCode,
        CancellationToken cancellationToken = default);

    /// <summary>Enables (re-activates) a cancelled or non-renewing subscription.</summary>
    /// <param name="request">The subscription code and its email token.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/subscription/enable")]
    Task<ApiResponse<PayStackResponse<object>>> EnableAsync(
        [Body] EnableSubscriptionRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Disables a subscription, stopping future charges. This is Paystack's only cancellation
    /// mechanism — there is no DELETE endpoint for subscriptions.
    /// </summary>
    /// <param name="request">The subscription code and its email token.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/subscription/disable")]
    Task<ApiResponse<PayStackResponse<object>>> DisableAsync(
        [Body] DisableSubscriptionRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Generates a hosted link the customer can visit to update their subscription's card/authorization.</summary>
    /// <param name="code">The subscription code.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/subscription/{code}/manage/link/")]
    Task<ApiResponse<PayStackResponse<SubscriptionLinkData>>> GenerateUpdateLinkAsync(
        string code,
        CancellationToken cancellationToken = default);

    /// <summary>Emails the customer a hosted link to update their subscription's card/authorization.</summary>
    /// <param name="code">The subscription code.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/subscription/{code}/manage/email/")]
    Task<ApiResponse<PayStackResponse<object>>> SendUpdateLinkAsync(
        string code,
        CancellationToken cancellationToken = default);
}
