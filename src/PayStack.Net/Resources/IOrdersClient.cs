using PayStack.Net.Models.Common;
using PayStack.Net.Models.Requests;
using PayStack.Net.Models.Responses;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Orders API: create and manage "pay for me" orders.
/// See <see href="https://paystack.com/docs/api/order/">Paystack API reference — Orders</see>.
/// </summary>
public interface IOrdersClient
{
    /// <summary>Creates a new order for a customer from one or more product line items.</summary>
    /// <param name="request">The customer and the product line items that make up the order.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/order")]
    Task<ApiResponse<PayStackResponse<OrderData>>> CreateAsync(
        [Body] CreateOrderRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Lists orders on your integration, most recent first.</summary>
    /// <param name="perPage">Records per page. Defaults to 50.</param>
    /// <param name="page">Page number (offset pagination). Defaults to 1.</param>
    /// <param name="from">Filter to orders created on or after this timestamp.</param>
    /// <param name="to">Filter to orders created on or before this timestamp.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/order")]
    Task<ApiResponse<PayStackResponse<List<OrderData>>>> ListAsync(
        [Query] int? perPage = null,
        [Query] int? page = null,
        [Query] DateTimeOffset? from = null,
        [Query] DateTimeOffset? to = null,
        CancellationToken cancellationToken = default);

    /// <summary>Fetches a single order by its Paystack id.</summary>
    /// <param name="id">The order's numeric id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/order/{id}")]
    Task<ApiResponse<PayStackResponse<OrderData>>> FetchAsync(
        long id,
        CancellationToken cancellationToken = default);

    /// <summary>Lists orders that include a given product.</summary>
    /// <param name="id">The product's numeric id.</param>
    /// <param name="perPage">Records per page. Defaults to 50.</param>
    /// <param name="page">Page number (offset pagination). Defaults to 1.</param>
    /// <param name="from">Filter to orders created on or after this timestamp.</param>
    /// <param name="to">Filter to orders created on or before this timestamp.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/order/product/{id}")]
    Task<ApiResponse<PayStackResponse<List<OrderData>>>> FetchProductOrdersAsync(
        long id,
        [Query] int? perPage = null,
        [Query] int? page = null,
        [Query] DateTimeOffset? from = null,
        [Query] DateTimeOffset? to = null,
        CancellationToken cancellationToken = default);

    /// <summary>Validates a "pay for me" order by its code, returning whether it is still payable.</summary>
    /// <param name="code">The order's public code.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/order/{code}/validate")]
    Task<ApiResponse<PayStackResponse<OrderValidationData>>> ValidateAsync(
        string code,
        CancellationToken cancellationToken = default);
}
