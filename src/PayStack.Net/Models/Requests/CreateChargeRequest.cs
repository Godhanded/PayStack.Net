namespace PayStack.Net.Models.Requests;

/// <summary>
/// Request body for <c>POST /charge</c> — charges a customer directly on a specific channel
/// (bank, USSD, mobile money, QR, EFT, Capitec Pay) or via a stored authorization, without a
/// hosted checkout redirect. Set exactly one of the channel-specific detail objects (or
/// <see cref="AuthorizationCode"/>) depending on how you want to charge the customer.
/// </summary>
public sealed class CreateChargeRequest
{
    /// <summary>Customer's email address. Required.</summary>
    public required string Email { get; set; }

    /// <summary>Amount to charge, in the currency's subunit. Required.</summary>
    public required string Amount { get; set; }

    /// <summary>Split code of a pre-created transaction split to apply.</summary>
    public string? SplitCode { get; set; }

    /// <summary>Subaccount code the payment should be split with.</summary>
    public string? Subaccount { get; set; }

    /// <summary>Flat fee (in subunits) to charge in addition to the normal Paystack fee when using <see cref="Subaccount"/>.</summary>
    public int? TransactionCharge { get; set; }

    /// <summary>Who bears the Paystack fee for a split payment: "account" (default) or "subaccount".</summary>
    public string? Bearer { get; set; }

    /// <summary>Charge a bank account directly.</summary>
    public ChargeBankDetails? Bank { get; set; }

    /// <summary>Charge via a Pay with Bank Transfer (dynamic virtual account).</summary>
    public ChargeBankTransferDetails? BankTransfer { get; set; }

    /// <summary>Charge via USSD.</summary>
    public ChargeUssdDetails? Ussd { get; set; }

    /// <summary>Charge via mobile money.</summary>
    public ChargeMobileMoneyDetails? MobileMoney { get; set; }

    /// <summary>Charge via a scan-to-pay QR code.</summary>
    public ChargeQrDetails? Qr { get; set; }

    /// <summary>Charge via EFT.</summary>
    public ChargeEftDetails? Eft { get; set; }

    /// <summary>Charge via Capitec Pay.</summary>
    public ChargeCapitecPayDetails? CapitecPay { get; set; }

    /// <summary>A previously stored, reusable authorization code to charge instead of a raw channel.</summary>
    public string? AuthorizationCode { get; set; }

    /// <summary>4-digit card PIN, when required up front by the channel.</summary>
    public string? Pin { get; set; }

    /// <summary>Arbitrary JSON-serializable data to attach to the charge.</summary>
    public object? Metadata { get; set; }

    /// <summary>Unique transaction reference. Auto-generated when omitted.</summary>
    public string? Reference { get; set; }

    /// <summary>Identifier of the device used to initiate the charge, for fraud analysis.</summary>
    public string? DeviceId { get; set; }
}

/// <summary>Bank account details for a direct bank charge.</summary>
public sealed class ChargeBankDetails
{
    /// <summary>Paystack bank code.</summary>
    public required string Code { get; set; }

    public required string AccountNumber { get; set; }

    /// <summary>Kuda-only: the account holder's phone number.</summary>
    public string? Phone { get; set; }

    /// <summary>Kuda-only: the 6-digit OTP token.</summary>
    public string? Token { get; set; }
}

/// <summary>Options for a Pay with Bank Transfer charge.</summary>
public sealed class ChargeBankTransferDetails
{
    /// <summary>When the generated virtual account should expire, as an ISO 8601 timestamp string.</summary>
    public string? AccountExpiresAt { get; set; }
}

/// <summary>Options for a USSD charge.</summary>
public sealed class ChargeUssdDetails
{
    /// <summary>USSD channel type, e.g. "737".</summary>
    public required string Type { get; set; }
}

/// <summary>Details for a mobile money charge.</summary>
public sealed class ChargeMobileMoneyDetails
{
    public required string Phone { get; set; }

    public required string Account { get; set; }

    /// <summary>Mobile money network. Use <see cref="Common.PayStackMobileMoneyProvider"/> constants.</summary>
    public required string Provider { get; set; }
}

/// <summary>Options for a scan-to-pay QR charge.</summary>
public sealed class ChargeQrDetails
{
    /// <summary>QR provider, e.g. "scan-to-pay".</summary>
    public required string Provider { get; set; }
}

/// <summary>Options for an EFT charge.</summary>
public sealed class ChargeEftDetails
{
    /// <summary>EFT provider, e.g. "ozow".</summary>
    public required string Provider { get; set; }
}

/// <summary>Details for a Capitec Pay charge.</summary>
public sealed class ChargeCapitecPayDetails
{
    /// <summary>Which kind of identifier <see cref="IdentifierValue"/> is. Use <see cref="Common.PayStackCapitecIdentifierKey"/> constants.</summary>
    public required string IdentifierKey { get; set; }

    public required string IdentifierValue { get; set; }
}
