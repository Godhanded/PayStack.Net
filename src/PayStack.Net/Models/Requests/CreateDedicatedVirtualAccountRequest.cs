namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /dedicated_account</c>.</summary>
public sealed class CreateDedicatedVirtualAccountRequest
{
    /// <summary>Paystack customer id or customer code to attach the dedicated virtual account to. Required.</summary>
    public required string Customer { get; set; }

    /// <summary>The bank slug to create the dedicated account with, e.g. "wema-bank", "titan-paystack".</summary>
    public string? PreferredBank { get; set; }

    /// <summary>Subaccount code that transactions on this dedicated account should be split with.</summary>
    public string? Subaccount { get; set; }

    /// <summary>Split code of a pre-created transaction split to apply to transactions on this dedicated account.</summary>
    public string? SplitCode { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Phone { get; set; }
}
