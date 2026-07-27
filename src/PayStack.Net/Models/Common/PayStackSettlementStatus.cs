namespace PayStack.Net.Models.Common;

/// <summary>Well-known values for <see cref="Responses.SettlementData.Status"/>. See <see cref="PayStackChannel"/> for the rationale on using string constants instead of an enum.</summary>
public static class PayStackSettlementStatus
{
    public const string Success = "success";
    public const string Processing = "processing";
    public const string Pending = "pending";
    public const string Failed = "failed";
}
