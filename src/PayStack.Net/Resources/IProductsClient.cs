using PayStack.Net.Models.Requests;
using PayStack.Net.Models.Responses;
using PayStack.Net.Models.Common;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Products API: create and manage sellable products, e.g. for use with Storefronts or Payment Pages.
/// See <see href="https://paystack.com/docs/api/product/">Paystack API reference — Products</see>.
/// </summary>
public interface IProductsClient
{
    /// <summary>Creates a new product.</summary>
    /// <param name="request">Name, description, price, currency, and optional stock configuration.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/product")]
    Task<ApiResponse<PayStackResponse<ProductData>>> CreateAsync(
        [Body] CreateProductRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Lists products on your integration.</summary>
    /// <param name="perPage">Records per page. Defaults to 50.</param>
    /// <param name="page">Page number (offset pagination). Defaults to 1.</param>
    /// <param name="from">Filter to products created on or after this timestamp.</param>
    /// <param name="to">Filter to products created on or before this timestamp.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/product")]
    Task<ApiResponse<PayStackResponse<List<ProductData>>>> ListAsync(
        [Query] int? perPage = null,
        [Query] int? page = null,
        [Query] DateTimeOffset? from = null,
        [Query] DateTimeOffset? to = null,
        CancellationToken cancellationToken = default);

    /// <summary>Fetches a single product by its numeric id.</summary>
    /// <param name="id">The product's numeric id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/product/{id}")]
    Task<ApiResponse<PayStackResponse<ProductData>>> FetchAsync(
        long id,
        CancellationToken cancellationToken = default);

    /// <summary>Updates a product.</summary>
    /// <param name="id">The product's numeric id.</param>
    /// <param name="request">Full replacement of the product's name, description, price, and currency, plus optional stock fields.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Put("/product/{id}")]
    Task<ApiResponse<PayStackResponse<ProductData>>> UpdateAsync(
        long id,
        [Body] UpdateProductRequest request,
        CancellationToken cancellationToken = default);
}
