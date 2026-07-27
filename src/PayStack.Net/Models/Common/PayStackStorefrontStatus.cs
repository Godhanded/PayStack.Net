namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known values for a storefront's <c>status</c> field. Modeled as string constants rather
/// than a C# <c>enum</c> so the SDK stays forward-compatible when Paystack adds a new status — you
/// can still pass any raw string, these are just the documented ones with IntelliSense support.
/// </summary>
public static class PayStackStorefrontStatus
{
    public const string Active = "active";
    public const string Inactive = "inactive";
}
