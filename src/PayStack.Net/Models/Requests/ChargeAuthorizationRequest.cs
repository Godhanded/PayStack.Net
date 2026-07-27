namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /transaction/charge_authorization</c> — charges a previously stored, reusable authorization without customer interaction.</summary>
public sealed class ChargeAuthorizationRequest
{
    /// <summary>Amount to charge, in the currency's subunit. Required.</summary>
    public required string Amount { get; set; }

    /// <summary>Customer's email address. Required — must match the email tied to the authorization.</summary>
    public required string Email { get; set; }

    /// <summary>The reusable authorization code from a previous successful transaction. Required.</summary>
    public required string AuthorizationCode { get; set; }

    /// <summary>Unique transaction reference. Auto-generated when omitted.</summary>
    public string? Reference { get; set; }

    /// <summary>ISO 4217 currency code. Defaults to the currency the authorization was created with.</summary>
    public string? Currency { get; set; }

    /// <summary>Arbitrary JSON-serializable data to attach to the transaction.</summary>
    public object? Metadata { get; set; }

    /// <summary>Restricts which payment channels are attempted. Use <see cref="Common.PayStackChannel"/> constants.</summary>
    public List<string>? Channels { get; set; }

    /// <summary>Subaccount code to split this charge with.</summary>
    public string? Subaccount { get; set; }

    /// <summary>Flat fee (in subunits) to charge in addition to the normal Paystack fee when using <see cref="Subaccount"/>.</summary>
    public int? TransactionCharge { get; set; }

    /// <summary>Who bears the Paystack fee for a split payment: "account" (default) or "subaccount".</summary>
    public string? Bearer { get; set; }

    /// <summary>When true, the charge is queued instead of processed synchronously — recommended for bulk/background charging.</summary>
    public bool? Queue { get; set; }
}
