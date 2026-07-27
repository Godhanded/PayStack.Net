namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known values for <see cref="Responses.BulkChargeBatchData.Status"/>. See
/// <see cref="PayStackChannel"/> for the rationale on using string constants instead of an enum.
/// </summary>
public static class PayStackBulkChargeBatchStatus
{
    public const string Active = "active";
    public const string Paused = "paused";
    public const string Complete = "complete";
}
