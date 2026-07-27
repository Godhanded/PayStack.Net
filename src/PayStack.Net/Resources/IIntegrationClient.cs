using PayStack.Net.Models.Common;
using PayStack.Net.Models.Requests;
using PayStack.Net.Models.Responses;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Integration API: manage integration-wide settings such as the checkout session timeout.
/// See <see href="https://paystack.com/docs/api/integration/">Paystack API reference — Integration</see>.
/// </summary>
public interface IIntegrationClient
{
    /// <summary>Fetches the current payment session timeout, in seconds.</summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/integration/payment_session_timeout")]
    Task<ApiResponse<PayStackResponse<PaymentSessionTimeoutData>>> FetchPaymentSessionTimeoutAsync(
        CancellationToken cancellationToken = default);

    /// <summary>Updates the payment session timeout. Set the timeout to 0 to cancel it entirely.</summary>
    /// <param name="request">The new timeout, in seconds.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Put("/integration/payment_session_timeout")]
    Task<ApiResponse<PayStackResponse<PaymentSessionTimeoutData>>> UpdatePaymentSessionTimeoutAsync(
        [Body] UpdatePaymentSessionTimeoutRequest request,
        CancellationToken cancellationToken = default);
}
