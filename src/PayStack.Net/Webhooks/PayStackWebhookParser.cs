using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PayStack.Net.Configuration;

namespace PayStack.Net.Webhooks;

/// <inheritdoc cref="IPayStackWebhookParser"/>
internal sealed class PayStackWebhookParser : IPayStackWebhookParser
{
    private readonly IOptionsMonitor<PayStackOptions> _options;
    private readonly ILogger<PayStackWebhookParser> _logger;

    public PayStackWebhookParser(IOptionsMonitor<PayStackOptions> options, ILogger<PayStackWebhookParser> logger)
    {
        _options = options;
        _logger = logger;
    }

    public PayStackWebhookEvent? Parse(string rawRequestBody, string? signatureHeaderValue)
    {
        var options = _options.CurrentValue;
        var secretKey = options.WebhookSecretKey ?? options.SecretKey;

        if (!PayStackWebhookSignatureVerifier.Verify(rawRequestBody, signatureHeaderValue, secretKey))
        {
            _logger.LogWarning("Rejected Paystack webhook: signature verification failed.");
            return null;
        }

        try
        {
            using var document = JsonDocument.Parse(rawRequestBody);
            var root = document.RootElement;

            if (!root.TryGetProperty("event", out var eventProperty) || eventProperty.ValueKind != JsonValueKind.String)
            {
                _logger.LogWarning("Rejected Paystack webhook: signature valid but body has no string \"event\" property.");
                return null;
            }

            if (!root.TryGetProperty("data", out var dataProperty))
            {
                _logger.LogWarning("Rejected Paystack webhook: signature valid but body has no \"data\" property.");
                return null;
            }

            var eventType = eventProperty.GetString()!;
            _logger.LogInformation("Received Paystack webhook {EventType}", eventType);

            return new PayStackWebhookEvent
            {
                Event = eventType,
                Data = dataProperty.Clone(),
            };
        }
        catch (JsonException ex)
        {
            _logger.LogWarning(ex, "Rejected Paystack webhook: signature valid but body is not valid JSON.");
            return null;
        }
    }
}
