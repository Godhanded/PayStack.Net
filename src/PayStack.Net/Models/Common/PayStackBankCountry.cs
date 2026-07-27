namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known country values accepted by the "list banks" <c>country</c> filter.
/// Modeled as string constants rather than a C# <c>enum</c> so the SDK stays forward-compatible
/// when Paystack adds a new country.
/// </summary>
public static class PayStackBankCountry
{
    public const string Ghana = "ghana";
    public const string Kenya = "kenya";
    public const string Nigeria = "nigeria";
    public const string SouthAfrica = "south africa";
}
