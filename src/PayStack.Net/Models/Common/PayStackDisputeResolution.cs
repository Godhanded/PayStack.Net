namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known resolution values accepted by <c>resolution</c> when resolving a dispute.
/// Modeled as string constants rather than a C# <c>enum</c> so the SDK stays forward-compatible
/// when Paystack adds a new resolution outcome.
/// </summary>
public static class PayStackDisputeResolution
{
    public const string MerchantAccepted = "merchant-accepted";
    public const string Declined = "declined";
}
