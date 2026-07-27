namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /split/:id/subaccount/remove</c>.</summary>
public sealed class RemoveSplitSubaccountRequest
{
    /// <summary>The subaccount code to remove from the split. Required.</summary>
    public required string Subaccount { get; set; }
}
