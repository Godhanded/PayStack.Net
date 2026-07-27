using PayStack.Net.Models.Common;
using PayStack.Net.Models.Requests;
using PayStack.Net.Models.Responses;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Payment Pages API: create hosted, shareable pages that collect payments without custom checkout code.
/// See <see href="https://paystack.com/docs/api/page/">Paystack API reference — Payment Pages</see>.
/// </summary>
public interface IPaymentPagesClient
{
    /// <summary>Creates a new payment page.</summary>
    /// <param name="request">Page name and optional configuration (amount, currency, type, branding, etc.).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/page")]
    Task<ApiResponse<PayStackResponse<PaymentPageData>>> CreateAsync(
        [Body] CreatePaymentPageRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Lists payment pages on your integration, most recent first.</summary>
    /// <param name="perPage">Records per page. Defaults to 50.</param>
    /// <param name="page">Page number (offset pagination). Defaults to 1.</param>
    /// <param name="from">Filter to pages created on or after this timestamp.</param>
    /// <param name="to">Filter to pages created on or before this timestamp.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/page")]
    Task<ApiResponse<PayStackResponse<List<PaymentPageData>>>> ListAsync(
        [Query] int? perPage = null,
        [Query] int? page = null,
        [Query] DateTimeOffset? from = null,
        [Query] DateTimeOffset? to = null,
        CancellationToken cancellationToken = default);

    /// <summary>Fetches a single payment page by its numeric id or its slug.</summary>
    /// <param name="idOrSlug">The page's numeric id or URL slug.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/page/{idOrSlug}")]
    Task<ApiResponse<PayStackResponse<PaymentPageData>>> FetchAsync(
        string idOrSlug,
        CancellationToken cancellationToken = default);

    /// <summary>Updates a payment page's name, description, amount, or active state.</summary>
    /// <param name="idOrSlug">The page's numeric id or URL slug.</param>
    /// <param name="request">Fields to update. Setting <c>Active</c> to <c>false</c> deactivates the page.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Put("/page/{idOrSlug}")]
    Task<ApiResponse<PayStackResponse<PaymentPageData>>> UpdateAsync(
        string idOrSlug,
        [Body] UpdatePaymentPageRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Checks whether a URL slug is available for use on a new payment page.</summary>
    /// <param name="slug">The slug to check.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/page/check_slug_availability/{slug}")]
    Task<ApiResponse<PayStackResponse<object?>>> CheckSlugAvailabilityAsync(
        string slug,
        CancellationToken cancellationToken = default);

    /// <summary>Adds one or more products to a payment page, turning it into a product/storefront page.</summary>
    /// <param name="id">The page's numeric id.</param>
    /// <param name="request">The ids of the products to attach.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/page/{id}/product")]
    Task<ApiResponse<PayStackResponse<PaymentPageData>>> AddProductsAsync(
        long id,
        [Body] AddProductsToPaymentPageRequest request,
        CancellationToken cancellationToken = default);
}
