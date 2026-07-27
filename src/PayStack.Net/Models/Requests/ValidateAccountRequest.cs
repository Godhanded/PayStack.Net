namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /bank/validate</c>.</summary>
public sealed class ValidateAccountRequest
{
    public required string AccountName { get; set; }

    public required string AccountNumber { get; set; }

    /// <summary>See <see cref="Common.PayStackAccountType"/>. Required.</summary>
    public required string AccountType { get; set; }

    public required string BankCode { get; set; }

    /// <summary>2-letter ISO country code, e.g. "ZA". Required.</summary>
    public required string CountryCode { get; set; }

    /// <summary>See <see cref="Common.PayStackDocumentType"/>. Required.</summary>
    public required string DocumentType { get; set; }

    /// <summary>The document number matching <see cref="DocumentType"/>.</summary>
    public string? DocumentNumber { get; set; }
}
