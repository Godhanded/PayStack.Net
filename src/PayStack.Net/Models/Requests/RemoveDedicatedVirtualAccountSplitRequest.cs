namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>DELETE /dedicated_account/split</c>.</summary>
public sealed class RemoveDedicatedVirtualAccountSplitRequest
{
    /// <summary>The account number of the dedicated virtual account to remove the split from. Required.</summary>
    public required string AccountNumber { get; set; }
}
