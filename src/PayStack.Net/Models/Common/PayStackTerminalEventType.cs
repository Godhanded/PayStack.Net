namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known event types accepted by <c>type</c> when sending a terminal event.
/// Modeled as string constants rather than a C# <c>enum</c> so the SDK stays forward-compatible
/// when Paystack adds a new event type.
/// </summary>
public static class PayStackTerminalEventType
{
    public const string Invoice = "invoice";
    public const string Transaction = "transaction";
}
