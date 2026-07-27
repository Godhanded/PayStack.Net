namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known values for <see cref="Responses.BulkChargeItemData.Status"/> and the
/// <c>status</c> filter on Fetch Charges in a Batch. See <see cref="PayStackChannel"/> for the
/// rationale on using string constants instead of an enum.
/// </summary>
public static class PayStackBulkChargeItemStatus
{
    public const string Pending = "pending";
    public const string Success = "success";
    public const string Failed = "failed";
}
