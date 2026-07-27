namespace PayStack.Net.Models.Common;

/// <summary>
/// A reusable (or single-use) card/bank authorization returned by Paystack after a successful charge.
/// When <see cref="Reusable"/> is <c>true</c>, <see cref="AuthorizationCode"/> can be charged again later
/// via <c>POST /transaction/charge_authorization</c> without the customer re-entering their details.
/// </summary>
public sealed class AuthorizationData
{
    /// <summary>Opaque token identifying this authorization. Store this to charge the customer again later.</summary>
    public string? AuthorizationCode { get; set; }

    /// <summary>First 6 digits of the card number, when the channel is card.</summary>
    public string? Bin { get; set; }

    /// <summary>Last 4 digits of the card number, when the channel is card.</summary>
    public string? Last4 { get; set; }

    /// <summary>Card expiry month ("MM"), when the channel is card.</summary>
    public string? ExpMonth { get; set; }

    /// <summary>Card expiry year ("YYYY"), when the channel is card.</summary>
    public string? ExpYear { get; set; }

    /// <summary>The payment channel this authorization was created on, e.g. "card", "bank".</summary>
    public string? Channel { get; set; }

    /// <summary>Card type, e.g. "visa", "verve", "mastercard".</summary>
    public string? CardType { get; set; }

    /// <summary>Issuing bank name.</summary>
    public string? Bank { get; set; }

    /// <summary>ISO country code of the issuing bank/card.</summary>
    public string? CountryCode { get; set; }

    /// <summary>Card brand, e.g. "visa".</summary>
    public string? Brand { get; set; }

    /// <summary>Whether <see cref="AuthorizationCode"/> can be charged again without customer input.</summary>
    public bool Reusable { get; set; }

    /// <summary>Signature uniquely identifying the underlying card/account across authorizations.</summary>
    public string? Signature { get; set; }

    /// <summary>Cardholder / account name, when available.</summary>
    public string? AccountName { get; set; }
}
