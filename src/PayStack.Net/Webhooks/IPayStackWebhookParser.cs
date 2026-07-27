namespace PayStack.Net.Webhooks;

/// <summary>
/// Verifies and parses inbound Paystack webhook deliveries. Resolve this from DI (registered by
/// <c>AddPayStack</c>) so it automatically uses the configured secret key, rather than calling
/// <see cref="PayStackWebhookSignatureVerifier"/> directly.
/// </summary>
public interface IPayStackWebhookParser
{
    /// <summary>
    /// Verifies the <c>x-paystack-signature</c> header against the raw body and, if valid, parses
    /// the event envelope.
    /// </summary>
    /// <param name="rawRequestBody">The exact, unmodified request body as received.</param>
    /// <param name="signatureHeaderValue">The value of the <c>x-paystack-signature</c> request header.</param>
    /// <returns>
    /// The parsed <see cref="PayStackWebhookEvent"/> when the signature is valid and the body is a
    /// well-formed event envelope; <c>null</c> when the signature is invalid, missing, or the body
    /// could not be parsed. Callers should return HTTP 401/400 (or simply ignore the request) when this returns <c>null</c>.
    /// </returns>
    PayStackWebhookEvent? Parse(string rawRequestBody, string? signatureHeaderValue);
}
