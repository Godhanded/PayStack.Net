using System.Text.Json;
using System.Text.Json.Serialization;
using PayStack.Net.Http;

namespace PayStack.Net;

/// <summary>
/// The <see cref="JsonSerializerOptions"/> shared by the Refit clients and the webhook event
/// deserializer, so request/response payloads and webhook payloads round-trip identically.
/// Paystack's JSON uses snake_case field names (e.g. <c>authorization_url</c>,
/// <c>recipient_code</c>) and string-encoded enums.
/// </summary>
public static class PayStackJsonOptions
{
    /// <summary>The canonical serializer options used throughout the SDK.</summary>
    public static readonly JsonSerializerOptions Default = Create();

    private static JsonSerializerOptions Create()
    {
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
        };
        options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower));
        options.Converters.Add(new LenientBooleanConverter());
        options.Converters.Add(new LenientNullableBooleanConverter());
        return options;
    }
}
