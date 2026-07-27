namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known settlement schedule values accepted by the Subaccounts API.
/// Modeled as string constants rather than a C# <c>enum</c> so the SDK stays forward-compatible
/// when Paystack adds a new schedule.
/// </summary>
public static class PayStackSettlementSchedule
{
    public const string Auto = "auto";
    public const string Weekly = "weekly";
    public const string Monthly = "monthly";
    public const string Manual = "manual";
}
