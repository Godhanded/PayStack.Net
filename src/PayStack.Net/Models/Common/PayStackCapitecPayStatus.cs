namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known status values returned by a Capitec Pay transaction requery.
/// Modeled as string constants rather than a C# <c>enum</c> so the SDK stays forward-compatible
/// when Paystack adds a new status.
/// </summary>
public static class PayStackCapitecPayStatus
{
    public const string Success = "success";
    public const string Pending = "pending";
    public const string Failed = "failed";
}
