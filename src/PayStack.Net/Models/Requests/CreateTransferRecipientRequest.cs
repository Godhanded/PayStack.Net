namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /transferrecipient</c>. Creating a recipient with an account number that already exists returns the existing record instead of erroring.</summary>
public sealed class CreateTransferRecipientRequest
{
    /// <summary>Recipient type. Use <see cref="Common.PayStackTransferRecipientType"/> constants. Required.</summary>
    public required string Type { get; set; }

    /// <summary>Recipient's name. Required.</summary>
    public required string Name { get; set; }

    /// <summary>Bank account number. Required unless <see cref="Type"/> is "authorization".</summary>
    public string? AccountNumber { get; set; }

    /// <summary>Bank code, obtained from the List Banks endpoint. Required unless <see cref="Type"/> is "authorization".</summary>
    public string? BankCode { get; set; }

    public string? Description { get; set; }

    /// <summary>ISO 4217 currency code.</summary>
    public string? Currency { get; set; }

    /// <summary>Authorization code from a previous transaction, used when <see cref="Type"/> is "authorization".</summary>
    public string? AuthorizationCode { get; set; }

    /// <summary>Arbitrary JSON-serializable data to attach to the recipient.</summary>
    public object? Metadata { get; set; }
}
