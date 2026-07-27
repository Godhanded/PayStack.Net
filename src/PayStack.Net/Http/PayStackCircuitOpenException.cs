namespace PayStack.Net.Http;

/// <summary>
/// Thrown when the resilience pipeline's circuit breaker is open because recent calls to Paystack
/// have been failing (timeouts, connection failures, or 5xx/429 responses past the configured
/// <c>CircuitBreakerFailureThreshold</c>). The circuit short-circuits new calls for
/// <c>CircuitBreakerBreakDuration</c> to avoid piling more load onto a struggling upstream, so this
/// exception means "Paystack (or the network path to it) is currently unreachable" rather than any
/// particular request being invalid — retry later, ideally with backoff, rather than immediately.
/// </summary>
public sealed class PayStackCircuitOpenException : Exception
{
    public PayStackCircuitOpenException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

/// <summary>
/// Thrown when a call to Paystack did not complete within the configured attempt or total timeout.
/// This indicates a transient network/availability problem rather than a rejected request — no
/// response was received from Paystack to inspect.
/// </summary>
public sealed class PayStackTimeoutException : Exception
{
    public PayStackTimeoutException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
