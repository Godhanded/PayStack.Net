using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using PayStack.Net.Configuration;

namespace PayStack.Net.Http;

/// <summary>
/// Attaches the <c>Authorization: Bearer &lt;secret key&gt;</c> header required by every Paystack
/// endpoint. Kept as a handler (rather than a static default header) so the secret key is re-read
/// from <see cref="IOptionsMonitor{TOptions}"/> on every request, allowing key rotation without
/// rebuilding the HTTP client.
/// </summary>
internal sealed class PayStackAuthHandler : DelegatingHandler
{
    private readonly IOptionsMonitor<PayStackOptions> _options;

    public PayStackAuthHandler(IOptionsMonitor<PayStackOptions> options)
    {
        _options = options;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var secretKey = _options.CurrentValue.SecretKey;
        if (string.IsNullOrWhiteSpace(secretKey))
        {
            throw new InvalidOperationException(
                "PayStackOptions.SecretKey is not configured. Set it via AddPayStack(options => options.SecretKey = \"sk_test_...\") " +
                "or bind the \"PayStack\" configuration section.");
        }

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", secretKey);
        return base.SendAsync(request, cancellationToken);
    }
}
