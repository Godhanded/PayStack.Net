using PayStack.Net.Models.Common;
using PayStack.Net.Models.Responses;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Capitec Pay API (South Africa only): requery the status of a Capitec Pay transaction.
/// See <see href="https://paystack.com/docs/api/capitec-pay/">Paystack API reference — Capitec Pay</see>.
/// </summary>
/// <remarks>
/// <para>
/// <b>This endpoint authenticates differently from every other resource in this SDK.</b> Paystack
/// documents it as callable from the frontend, so it expects <c>Authorization: Bearer &lt;PUBLIC_KEY&gt;</c>
/// instead of the secret key used everywhere else. The client configured via <c>PayStackOptions.SecretKey</c>
/// and wired up by this library's DI extensions sends the secret key on every request, including this one —
/// calling <see cref="RequeryAsync"/> through the standard <c>IPayStackClient</c>/DI-registered
/// <see cref="ICapitecPayClient"/> will therefore send the wrong key unless you specifically configure
/// this client with your public key (e.g. a separate <c>HttpClient</c>/named Refit client using
/// <c>PayStackOptions.PublicKey</c>). Double-check your setup before relying on this call in production.
/// </para>
/// </remarks>
public interface ICapitecPayClient
{
    /// <summary>
    /// Requeries a Capitec Pay transaction's status. Paystack recommends polling no sooner than
    /// 90 seconds after payment initiation, with at least a 10 second interval between polls
    /// (faster polls may return cached data).
    /// </summary>
    /// <param name="reference">The transaction reference to requery.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/capitec-pay/requery/{reference}")]
    Task<ApiResponse<PayStackResponse<CapitecPayRequeryData>>> RequeryAsync(
        string reference,
        CancellationToken cancellationToken = default);
}
