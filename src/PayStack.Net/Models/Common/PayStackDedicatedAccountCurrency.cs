namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known currency values accepted by the Dedicated Virtual Accounts <c>currency</c> filter.
/// Modeled as string constants rather than a C# <c>enum</c> so the SDK stays forward-compatible
/// when Paystack adds a new currency.
/// </summary>
public static class PayStackDedicatedAccountCurrency
{
    public const string Ngn = "NGN";
    public const string Ghs = "GHS";
}
