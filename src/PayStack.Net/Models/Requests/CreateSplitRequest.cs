namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /split</c>.</summary>
public sealed class CreateSplitRequest
{
    /// <summary>Name of the split. Required.</summary>
    public required string Name { get; set; }

    /// <summary>How subaccount shares are interpreted. Use <see cref="Common.PayStackSplitType"/> constants. Required.</summary>
    public required string Type { get; set; }

    /// <summary>ISO 4217 currency code. Required.</summary>
    public required string Currency { get; set; }

    /// <summary>The subaccounts participating in the split and their shares. Required.</summary>
    public required List<SplitSubaccountShare> Subaccounts { get; set; }

    /// <summary>Who bears the Paystack fee. Use <see cref="Common.PayStackBearerType"/> constants. Required.</summary>
    public required string BearerType { get; set; }

    /// <summary>The subaccount that bears the fee. Required when <see cref="BearerType"/> is "subaccount".</summary>
    public string? BearerSubaccount { get; set; }
}
