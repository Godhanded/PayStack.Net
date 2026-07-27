using Microsoft.Extensions.Options;
using PayStack.Net.Configuration;

namespace PayStack.Net.Http;

/// <summary>
/// Ensures state-changing requests (POST/PUT) carry an <c>Idempotency-Key</c> header, generating one
/// automatically when <see cref="PayStackOptions.AutoGenerateIdempotencyKeys"/> is enabled and the
/// caller hasn't already supplied one via a resource method's <c>idempotencyKey</c> parameter.
/// Paystack deduplicates retried requests that share a key, which protects transfer and refund calls
/// from being double-submitted when a response is lost to a network error and automatically retried.
/// </summary>
internal sealed class IdempotencyKeyHandler : DelegatingHandler
{
    private const string HeaderName = "Idempotency-Key";
    private readonly IOptionsMonitor<PayStackOptions> _options;

    public IdempotencyKeyHandler(IOptionsMonitor<PayStackOptions> options)
    {
        _options = options;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (_options.CurrentValue.AutoGenerateIdempotencyKeys
            && (request.Method == HttpMethod.Post || request.Method == HttpMethod.Put)
            && !request.Headers.Contains(HeaderName))
        {
            request.Headers.Add(HeaderName, Guid.NewGuid().ToString("N"));
        }

        return base.SendAsync(request, cancellationToken);
    }
}
