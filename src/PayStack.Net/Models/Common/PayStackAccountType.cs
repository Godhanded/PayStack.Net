namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known account type values accepted by the account validation endpoint.
/// Modeled as string constants rather than a C# <c>enum</c> so the SDK stays forward-compatible
/// when Paystack adds a new value.
/// </summary>
public static class PayStackAccountType
{
    public const string Personal = "personal";
    public const string Business = "business";
}
