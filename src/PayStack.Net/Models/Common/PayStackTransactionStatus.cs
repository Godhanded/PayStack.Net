namespace PayStack.Net.Models.Common;

/// <summary>Well-known values for <see cref="Responses.TransactionData.Status"/>. See <see cref="PayStackChannel"/> for the rationale on using string constants instead of an enum.</summary>
public static class PayStackTransactionStatus
{
    public const string Success = "success";
    public const string Failed = "failed";
    public const string Abandoned = "abandoned";
    public const string Reversed = "reversed";
    public const string Processing = "processing";
    public const string Queued = "queued";
    public const string Ongoing = "ongoing";
    public const string Pending = "pending";
}
