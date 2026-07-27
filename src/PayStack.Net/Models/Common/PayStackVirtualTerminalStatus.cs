namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known status values used by the <c>status</c> filter on the Virtual Terminal list endpoint.
/// Modeled as string constants rather than a C# <c>enum</c> so the SDK stays forward-compatible
/// when Paystack adds a new status.
/// </summary>
public static class PayStackVirtualTerminalStatus
{
    public const string Active = "active";
    public const string Inactive = "inactive";
}
