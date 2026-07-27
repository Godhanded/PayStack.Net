namespace PayStack.Net.Models.Requests;

/// <summary>
/// Request body for <c>POST /customer/:email_or_code/identification</c> — kicks off identity
/// validation for a customer. Only <c>bank_account</c> validation is currently supported. This is
/// asynchronous: Paystack returns 202 Accepted immediately and delivers the result via webhook.
/// </summary>
public sealed class ValidateCustomerRequest
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    /// <summary>Validation type. Only "bank_account" is currently supported. Required.</summary>
    public required string Type { get; set; }

    /// <summary>The value to validate, e.g. the BVN. Required.</summary>
    public required string Value { get; set; }

    /// <summary>2-letter ISO country code. Required.</summary>
    public required string Country { get; set; }

    /// <summary>Bank Verification Number. Required.</summary>
    public required string Bvn { get; set; }

    /// <summary>Required when <see cref="Type"/> is "bank_account".</summary>
    public string? BankCode { get; set; }

    /// <summary>Required when <see cref="Type"/> is "bank_account".</summary>
    public string? AccountNumber { get; set; }

    public string? MiddleName { get; set; }
}
