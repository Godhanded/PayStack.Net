using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace PayStack.Net.Http;

/// <summary>
/// Structured request/response logging for outbound Paystack API calls. Only ever logs the HTTP
/// method, path (query string stripped, since some endpoints accept sensitive filters), status code,
/// and elapsed time — never headers or bodies, so secret keys, customer PII, and card/bank details
/// never reach the log sink.
/// </summary>
internal sealed class PayStackLoggingHandler : DelegatingHandler
{
    private readonly ILogger<PayStackLoggingHandler> _logger;

    public PayStackLoggingHandler(ILogger<PayStackLoggingHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var method = request.Method;
        var path = request.RequestUri?.AbsolutePath ?? string.Empty;
        var stopwatch = Stopwatch.StartNew();

        _logger.LogDebug("Paystack request starting {Method} {Path}", method, path);

        try
        {
            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            stopwatch.Stop();

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation(
                    "Paystack request {Method} {Path} completed with {StatusCode} in {ElapsedMs}ms",
                    method, path, (int)response.StatusCode, stopwatch.ElapsedMilliseconds);
            }
            else
            {
                _logger.LogWarning(
                    "Paystack request {Method} {Path} failed with {StatusCode} in {ElapsedMs}ms",
                    method, path, (int)response.StatusCode, stopwatch.ElapsedMilliseconds);
            }

            return response;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            _logger.LogError(ex,
                "Paystack request {Method} {Path} threw after {ElapsedMs}ms",
                method, path, stopwatch.ElapsedMilliseconds);
            throw;
        }
    }
}
