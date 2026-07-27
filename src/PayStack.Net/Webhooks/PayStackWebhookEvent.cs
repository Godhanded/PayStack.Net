using System.Text.Json;

namespace PayStack.Net.Webhooks;

/// <summary>
/// A verified Paystack webhook event: <c>{ "event": "...", "data": { ... } }</c>. The shape of
/// <see cref="Data"/> depends on <see cref="Event"/> — use <see cref="GetData{T}"/> or
/// <see cref="TryGetData{T}"/> with the resource DTO that matches the event (see
/// <see cref="Models.Common.PayStackWebhookEventType"/> constants and their XML docs for the mapping,
/// e.g. <c>charge.success</c> → <c>PayStack.Net.Models.Responses.TransactionData</c>).
/// </summary>
public sealed class PayStackWebhookEvent
{
    /// <summary>The dotted event type string, e.g. "charge.success". See <see cref="Models.Common.PayStackWebhookEventType"/> for known values.</summary>
    public required string Event { get; init; }

    /// <summary>
    /// The raw <c>data</c> payload. Its shape depends on <see cref="Event"/> — for events where
    /// <c>data</c> is an array (currently only <c>subscription.expiring_cards</c>), <see cref="JsonValueKind.Array"/>.
    /// </summary>
    public required JsonElement Data { get; init; }

    /// <summary>Deserializes <see cref="Data"/> into the given type using the SDK's shared JSON options. Throws if the shape doesn't match.</summary>
    public T GetData<T>() => Data.Deserialize<T>(PayStackJsonOptions.Default)
        ?? throw new InvalidOperationException($"Webhook event '{Event}' data deserialized to null.");

    /// <summary>Attempts to deserialize <see cref="Data"/> into the given type, returning <c>false</c> instead of throwing on failure.</summary>
    public bool TryGetData<T>(out T? data)
    {
        try
        {
            data = Data.Deserialize<T>(PayStackJsonOptions.Default);
            return data is not null;
        }
        catch (JsonException)
        {
            data = default;
            return false;
        }
    }
}
