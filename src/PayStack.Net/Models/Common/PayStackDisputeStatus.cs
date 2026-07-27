namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known dispute status values used by the <c>status</c> field/filter on the Disputes API.
/// Modeled as string constants rather than a C# <c>enum</c> so the SDK stays forward-compatible
/// when Paystack adds a new status.
/// </summary>
public static class PayStackDisputeStatus
{
    public const string AwaitingMerchantFeedback = "awaiting-merchant-feedback";
    public const string AwaitingBankFeedback = "awaiting-bank-feedback";
    public const string Pending = "pending";
    public const string Resolved = "resolved";
}
