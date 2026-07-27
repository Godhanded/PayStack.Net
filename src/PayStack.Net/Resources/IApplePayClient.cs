using PayStack.Net.Models.Common;
using PayStack.Net.Models.Requests;
using PayStack.Net.Models.Responses;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Apple Pay API: register and manage the domains allowed to present the Apple Pay button
/// on your checkout. See <see href="https://paystack.com/docs/api/apple-pay/">Paystack API reference — Apple Pay</see>.
/// </summary>
public interface IApplePayClient
{
    /// <summary>Registers a domain for use with Apple Pay. Only one domain can be registered per call.</summary>
    /// <param name="request">The domain name to register.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/apple-pay/domain")]
    Task<ApiResponse<PayStackResponse<object>>> RegisterDomainAsync(
        [Body] RegisterApplePayDomainRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Lists the domains registered for Apple Pay on your integration.</summary>
    /// <param name="useCursor">Whether to paginate using cursors.</param>
    /// <param name="next">Cursor to fetch the next page.</param>
    /// <param name="previous">Cursor to fetch the previous page.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/apple-pay/domain")]
    Task<ApiResponse<PayStackResponse<ApplePayDomainsData>>> ListDomainsAsync(
        [Query] bool? useCursor = null,
        [Query] string? next = null,
        [Query] string? previous = null,
        CancellationToken cancellationToken = default);

    /// <summary>Unregisters a domain previously registered for Apple Pay.</summary>
    /// <param name="request">The domain name to unregister.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Delete("/apple-pay/domain")]
    Task<ApiResponse<PayStackResponse<object>>> UnregisterDomainAsync(
        [Body] UnregisterApplePayDomainRequest request,
        CancellationToken cancellationToken = default);
}
