namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known values for a plan's <c>interval</c> field. Modeled as string constants rather than a
/// C# <c>enum</c> so the SDK stays forward-compatible when Paystack adds a new interval — you can
/// still pass any raw string, these are just the documented ones with IntelliSense support.
/// </summary>
public static class PayStackPlanInterval
{
    /// <summary>Only accepted on <c>PUT /plan/:id_or_code</c> (update), not on create.</summary>
    public const string Hourly = "hourly";
    public const string Daily = "daily";
    public const string Weekly = "weekly";
    public const string Monthly = "monthly";
    public const string Quarterly = "quarterly";
    public const string Biannually = "biannually";
    public const string Annually = "annually";
}
