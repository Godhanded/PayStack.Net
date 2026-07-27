namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /transfer</c>.</summary>
public sealed class InitiateTransferRequest
{
    /// <summary>Where the funds are sourced from. Currently only "balance" is supported. Required.</summary>
    public required string Source { get; set; }

    /// <summary>Amount to transfer, in the currency's subunit (e.g. kobo for NGN). Required.</summary>
    public required long Amount { get; set; }

    /// <summary>The recipient code ("RCP_xxx") of the transfer recipient. Required.</summary>
    public required string Recipient { get; set; }

    /// <summary>
    /// Unique transfer reference, 16-50 characters, restricted to letters, numbers, hyphens, and
    /// underscores. Required — this is the field to make retries safe; combine with
    /// <c>Idempotency-Key</c> for full protection against duplicate payouts.
    /// </summary>
    public required string Reference { get; set; }

    public string? Reason { get; set; }

    /// <summary>ISO 4217 currency code. Defaults to NGN.</summary>
    public string? Currency { get; set; }

    /// <summary>Required in Kenya when paying out to an MPESA Paybill/Till account reference.</summary>
    public string? AccountReference { get; set; }
}
