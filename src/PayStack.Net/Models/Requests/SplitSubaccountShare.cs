namespace PayStack.Net.Models.Requests;

/// <summary>
/// A subaccount and its share of a transaction split. Used both in <see cref="CreateSplitRequest.Subaccounts"/>
/// and as the body of <c>POST /split/:id/subaccount/add</c>.
/// </summary>
public sealed class SplitSubaccountShare
{
    /// <summary>The subaccount's code.</summary>
    public required string Subaccount { get; set; }

    /// <summary>The subaccount's share, interpreted per the split's <c>type</c> (percentage points or a flat subunit amount).</summary>
    public required int Share { get; set; }
}
