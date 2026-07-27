using System.Text.Json;
using System.Text.Json.Serialization;

namespace PayStack.Net.Http;

/// <summary>
/// Some Paystack list endpoints (e.g. Subaccounts) serialize boolean fields as the integers 0/1
/// instead of JSON <c>true</c>/<c>false</c>, inconsistently with the same field on other endpoints
/// for the same resource. System.Text.Json does not coerce numbers into <see cref="bool"/> by
/// default, so this converter accepts both encodings to avoid a <see cref="JsonException"/> depending
/// on which endpoint returned the payload.
/// </summary>
internal sealed class LenientBooleanConverter : JsonConverter<bool>
{
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        reader.TokenType switch
        {
            JsonTokenType.True => true,
            JsonTokenType.False => false,
            JsonTokenType.Number => reader.GetInt32() != 0,
            JsonTokenType.String => reader.GetString() is "1" or "true",
            _ => throw new JsonException($"Cannot convert token type {reader.TokenType} to bool."),
        };

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) =>
        writer.WriteBooleanValue(value);
}

/// <summary>Nullable counterpart of <see cref="LenientBooleanConverter"/> for <see cref="bool"/>? properties.</summary>
internal sealed class LenientNullableBooleanConverter : JsonConverter<bool?>
{
    public override bool? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        reader.TokenType switch
        {
            JsonTokenType.Null => null,
            JsonTokenType.True => true,
            JsonTokenType.False => false,
            JsonTokenType.Number => reader.GetInt32() != 0,
            JsonTokenType.String => reader.GetString() is "1" or "true",
            _ => throw new JsonException($"Cannot convert token type {reader.TokenType} to bool?."),
        };

    public override void Write(Utf8JsonWriter writer, bool? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
        {
            writer.WriteBooleanValue(value.Value);
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}
