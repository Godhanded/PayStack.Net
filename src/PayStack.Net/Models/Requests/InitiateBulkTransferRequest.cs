namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /transfer/bulk</c>. Requires the Transfers OTP requirement to be disabled on your integration.</summary>
public sealed class InitiateBulkTransferRequest
{
    /// <summary>Where the funds are sourced from. Currently only "balance" is supported. Required.</summary>
    public required string Source { get; set; }

    public string? Currency { get; set; }

    /// <summary>The individual transfers to initiate. Required.</summary>
    public required List<BulkTransferItem> Transfers { get; set; }
}

/// <summary>A single transfer entry within an <see cref="InitiateBulkTransferRequest"/>.</summary>
public sealed class BulkTransferItem
{
    /// <summary>Amount to transfer, in the currency's subunit. Required.</summary>
    public required long Amount { get; set; }

    /// <summary>Unique transfer reference, 16-50 characters. Optional — Paystack generates one when omitted.</summary>
    public string? Reference { get; set; }

    public string? Reason { get; set; }

    /// <summary>The recipient code ("RCP_xxx") of the transfer recipient. Required.</summary>
    public required string Recipient { get; set; }
}
