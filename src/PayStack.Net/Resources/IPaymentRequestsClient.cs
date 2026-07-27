using PayStack.Net.Models.Common;
using PayStack.Net.Models.Requests;
using PayStack.Net.Models.Responses;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Payment Requests API (invoicing): create, send, and track invoices for customers to pay.
/// See <see href="https://paystack.com/docs/api/payment-request/">Paystack API reference — Payment Requests</see>.
/// </summary>
public interface IPaymentRequestsClient
{
    /// <summary>Creates a new payment request (invoice) for a customer.</summary>
    /// <param name="request">Customer, amount (or line items/tax), and optional invoicing details.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/paymentrequest")]
    Task<ApiResponse<PayStackResponse<PaymentRequestData>>> CreateAsync(
        [Body] CreatePaymentRequestRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Lists payment requests on your integration, most recent first.</summary>
    /// <param name="query">Optional filters: pagination, customer, status, currency, archive inclusion, and date range.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/paymentrequest")]
    Task<ApiResponse<PayStackResponse<List<PaymentRequestData>>>> ListAsync(
        [Query] ListPaymentRequestsQuery? query = null,
        CancellationToken cancellationToken = default);

    /// <summary>Fetches a single payment request by its numeric id or its request code.</summary>
    /// <param name="idOrCode">The request's numeric id or its "PRQ_..." code.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/paymentrequest/{idOrCode}")]
    Task<ApiResponse<PayStackResponse<PaymentRequestData>>> FetchAsync(
        string idOrCode,
        CancellationToken cancellationToken = default);

    /// <summary>Verifies a payment request's details and current payment status by its code.</summary>
    /// <param name="code">The request's "PRQ_..." code.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/paymentrequest/verify/{code}")]
    Task<ApiResponse<PayStackResponse<PaymentRequestData>>> VerifyAsync(
        string code,
        CancellationToken cancellationToken = default);

    /// <summary>Sends (or resends) the payment request notification email to the customer.</summary>
    /// <param name="code">The request's "PRQ_..." code.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/paymentrequest/notify/{code}")]
    Task<ApiResponse<PayStackResponse<object?>>> SendNotificationAsync(
        string code,
        CancellationToken cancellationToken = default);

    /// <summary>Returns aggregate payment request totals (pending, successful, and overall), broken down by currency.</summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/paymentrequest/totals")]
    Task<ApiResponse<PayStackResponse<PaymentRequestTotalsData>>> GetTotalsAsync(
        CancellationToken cancellationToken = default);

    /// <summary>Finalizes a draft payment request, making it payable and optionally notifying the customer.</summary>
    /// <param name="code">The request's "PRQ_..." code.</param>
    /// <param name="request">Whether to notify the customer.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/paymentrequest/finalize/{code}")]
    Task<ApiResponse<PayStackResponse<PaymentRequestData>>> FinalizeAsync(
        string code,
        [Body] FinalizePaymentRequestRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Updates an existing payment request. Only the fields supplied on <paramref name="request"/> are changed.</summary>
    /// <param name="idOrCode">The request's numeric id or its "PRQ_..." code.</param>
    /// <param name="request">Fields to update.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Put("/paymentrequest/{idOrCode}")]
    Task<ApiResponse<PayStackResponse<PaymentRequestData>>> UpdateAsync(
        string idOrCode,
        [Body] UpdatePaymentRequestRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Archives a payment request, removing it from the default (non-archived) listing.</summary>
    /// <param name="code">The request's "PRQ_..." code.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/paymentrequest/archive/{code}")]
    Task<ApiResponse<PayStackResponse<object?>>> ArchiveAsync(
        string code,
        CancellationToken cancellationToken = default);
}
