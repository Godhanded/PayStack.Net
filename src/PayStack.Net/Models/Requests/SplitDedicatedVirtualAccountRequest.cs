namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /dedicated_account/split</c>.</summary>
public sealed class SplitDedicatedVirtualAccountRequest
{
    /// <summary>Paystack customer id or customer code owning the dedicated virtual account. Required.</summary>
    public required string Customer { get; set; }

    /// <summary>Subaccount code that transactions on this dedicated account should be split with.</summary>
    public string? Subaccount { get; set; }

    /// <summary>Split code of a pre-created transaction split to apply.</summary>
    public string? SplitCode { get; set; }

    /// <summary>The bank slug to create the dedicated account with.</summary>
    public string? PreferredBank { get; set; }
}
