using Polly.CircuitBreaker;
using Polly.Timeout;

namespace PayStack.Net.Http;

/// <summary>
/// Translates the Polly exceptions raised by the resilience pipeline (circuit breaker open, timeout)
/// into the SDK's own <see cref="PayStackCircuitOpenException"/> / <see cref="PayStackTimeoutException"/>
/// so callers can catch stable, documented types instead of depending on Polly's exception hierarchy.
/// Registered as the outermost handler so it sees exceptions from every inner handler, including the
/// resilience handler installed by <c>AddStandardResilienceHandler</c>.
/// </summary>
internal sealed class PayStackExceptionTranslationHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
        catch (BrokenCircuitException ex)
        {
            throw new PayStackCircuitOpenException(
                "The Paystack API circuit breaker is open due to recent transient failures. Try again shortly.", ex);
        }
        catch (TimeoutRejectedException ex)
        {
            throw new PayStackTimeoutException(
                "The request to the Paystack API did not complete within the configured timeout.", ex);
        }
    }
}
