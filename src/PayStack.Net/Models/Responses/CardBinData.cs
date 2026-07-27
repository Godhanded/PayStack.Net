namespace PayStack.Net.Models.Responses;

/// <summary>Card metadata resolved from a Bank Identification Number (BIN).</summary>
public sealed class CardBinData
{
    /// <summary>The first 6 digits of the card number that were resolved.</summary>
    public string Bin { get; set; } = string.Empty;

    /// <summary>Card brand, e.g. "visa", "mastercard".</summary>
    public string? Brand { get; set; }

    public string? SubBrand { get; set; }

    public string? CountryCode { get; set; }

    public string? CountryName { get; set; }

    /// <summary>Card type, e.g. "debit", "credit".</summary>
    public string? CardType { get; set; }

    /// <summary>Issuing bank name.</summary>
    public string? Bank { get; set; }

    public long? LinkedBankId { get; set; }
}
