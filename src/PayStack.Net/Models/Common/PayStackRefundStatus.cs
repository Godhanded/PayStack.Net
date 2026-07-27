namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known refund status values, used on the <c>status</c> field of a <c>RefundData</c>.
/// Modeled as string constants rather than a C# <c>enum</c> so the SDK stays forward-compatible
/// when Paystack adds a new status.
/// </summary>
public static class PayStackRefundStatus
{
    public const string Pending = "pending";
    public const string Processing = "processing";
    public const string Processed = "processed";
    public const string NeedsAttention = "needs-attention";
    public const string Failed = "failed";
}
