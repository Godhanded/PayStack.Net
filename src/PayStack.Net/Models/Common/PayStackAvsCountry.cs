namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known country values accepted by the address verification "list states" <c>country</c> filter.
/// Modeled as string constants rather than a C# <c>enum</c> so the SDK stays forward-compatible
/// when Paystack adds a new country.
/// </summary>
public static class PayStackAvsCountry
{
    public const string UnitedStates = "US";
    public const string Canada = "CA";
    public const string UnitedKingdom = "GB";
}
