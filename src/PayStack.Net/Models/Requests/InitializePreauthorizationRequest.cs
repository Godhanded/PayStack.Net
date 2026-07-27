namespace PayStack.Net.Models.Requests;

/// <summary>
/// Request body for <c>POST /preauthorization/initialize</c>. South Africa (ZAR) only — reserves an
/// authorization for a later capture instead of charging immediately.
/// </summary>
public sealed class InitializePreauthorizationRequest
{
    /// <summary>Amount to reserve, in the currency's subunit. Required.</summary>
    public required string Amount { get; set; }

    /// <summary>Customer's email address. Required.</summary>
    public required string Email { get; set; }

    /// <summary>ISO 4217 currency code. Only "ZAR" is supported. Required.</summary>
    public required string Currency { get; set; }

    /// <summary>Unique transaction reference. Auto-generated when omitted.</summary>
    public string? Reference { get; set; }

    public string? CallbackUrl { get; set; }

    /// <summary>Arbitrary metadata to attach, as a JSON-encoded string.</summary>
    public string? Metadata { get; set; }

    /// <summary>Split code of a pre-created transaction split to apply.</summary>
    public string? SplitCode { get; set; }

    /// <summary>Subaccount code the payment should be split with.</summary>
    public string? Subaccount { get; set; }

    /// <summary>Flat fee (in subunits) to charge in addition to the normal Paystack fee when using <see cref="Subaccount"/>.</summary>
    public int? TransactionCharge { get; set; }

    /// <summary>Who bears the Paystack fee for a split payment: "account" (default) or "subaccount".</summary>
    public string? Bearer { get; set; }

    /// <summary>What to do if the reservation is never captured or released. Use <see cref="Common.PayStackExpireAction"/> constants. Defaults to "release".</summary>
    public string? ExpireAction { get; set; }

    /// <summary>Days until the reservation expires (1-30). Defaults to 5.</summary>
    public int? ExpireAfterDays { get; set; }
}
