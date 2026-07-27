using PayStack.Net.Models.Common;
using PayStack.Net.Models.Requests;
using PayStack.Net.Models.Responses;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Storefronts API: create hosted storefronts that list products for customers to browse and buy.
/// See <see href="https://paystack.com/docs/api/storefront/">Paystack API reference — Storefronts</see>.
/// </summary>
public interface IStorefrontsClient
{
    /// <summary>Creates a new storefront.</summary>
    /// <param name="request">Name and optional slug/description.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/storefront")]
    Task<ApiResponse<PayStackResponse<StorefrontData>>> CreateAsync(
        [Body] CreateStorefrontRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Lists storefronts on your integration.</summary>
    /// <param name="perPage">Records per page. Defaults to 50.</param>
    /// <param name="page">Page number (offset pagination). Defaults to 1.</param>
    /// <param name="status">Filter by storefront status: "active" or "inactive". See <see cref="Models.Common.PayStackStorefrontStatus"/>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/storefront")]
    Task<ApiResponse<PayStackResponse<List<StorefrontData>>>> ListAsync(
        [Query] int? perPage = null,
        [Query] int? page = null,
        [Query] string? status = null,
        CancellationToken cancellationToken = default);

    /// <summary>Fetches a single storefront by its numeric id.</summary>
    /// <param name="id">The storefront's numeric id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/storefront/{id}")]
    Task<ApiResponse<PayStackResponse<StorefrontData>>> FetchAsync(
        long id,
        CancellationToken cancellationToken = default);

    /// <summary>Updates a storefront's name and/or description.</summary>
    /// <param name="id">The storefront's numeric id.</param>
    /// <param name="request">Fields to update; omit a field to leave it unchanged.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Put("/storefront/{id}")]
    Task<ApiResponse<PayStackResponse<StorefrontData>>> UpdateAsync(
        long id,
        [Body] UpdateStorefrontRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Deletes a storefront.</summary>
    /// <param name="id">The storefront's numeric id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Delete("/storefront/{id}")]
    Task<ApiResponse<PayStackResponse<object>>> DeleteAsync(
        long id,
        CancellationToken cancellationToken = default);

    /// <summary>Checks whether a storefront slug is available for use.</summary>
    /// <param name="slug">The slug to check.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/storefront/verify/{slug}")]
    Task<ApiResponse<PayStackResponse<object>>> VerifySlugAsync(
        string slug,
        CancellationToken cancellationToken = default);

    /// <summary>Fetches orders placed against a storefront.</summary>
    /// <param name="id">The storefront's numeric id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/storefront/{id}/order")]
    Task<ApiResponse<PayStackResponse<List<StorefrontOrderData>>>> FetchOrdersAsync(
        long id,
        CancellationToken cancellationToken = default);

    /// <summary>Adds existing products to a storefront's listing.</summary>
    /// <param name="id">The storefront's numeric id.</param>
    /// <param name="request">The Paystack product ids to add.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/storefront/{id}/product")]
    Task<ApiResponse<PayStackResponse<object>>> AddProductsAsync(
        long id,
        [Body] AddStorefrontProductsRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Lists the products currently listed on a storefront.</summary>
    /// <param name="id">The storefront's numeric id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/storefront/{id}/product")]
    Task<ApiResponse<PayStackResponse<List<StorefrontProductData>>>> ListProductsAsync(
        long id,
        CancellationToken cancellationToken = default);

    /// <summary>Publishes a storefront, making it publicly accessible.</summary>
    /// <param name="id">The storefront's numeric id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/storefront/{id}/publish")]
    Task<ApiResponse<PayStackResponse<object>>> PublishAsync(
        long id,
        CancellationToken cancellationToken = default);

    /// <summary>Duplicates an existing storefront, including its product listings.</summary>
    /// <param name="id">The numeric id of the storefront to duplicate.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/storefront/{id}/duplicate")]
    Task<ApiResponse<PayStackResponse<object>>> DuplicateAsync(
        long id,
        CancellationToken cancellationToken = default);
}
