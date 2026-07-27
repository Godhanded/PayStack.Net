using System.Text.Json;
using System.Text.Json.Serialization;
using PayStack.Net.Http;

namespace PayStack.Net;

/// <summary>
/// The <see cref="JsonSerializerOptions"/> shared by the Refit clients and the webhook event
/// deserializer, so request/response payloads and webhook payloads round-trip identically.
/// Paystack's JSON uses camelCase field names and string-encoded enums.
/// </summary>
public static class PayStackJsonOptions
{
    /// <summary>The canonical serializer options used throughout the SDK.</summary>
    public static readonly JsonSerializerOptions Default = Create();

    private static JsonSerializerOptions Create()
    {
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
        };
        options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        options.Converters.Add(new LenientBooleanConverter());
        options.Converters.Add(new LenientNullableBooleanConverter());
        return options;
    }
}
