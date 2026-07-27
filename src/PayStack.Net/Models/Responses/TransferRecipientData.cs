namespace PayStack.Net.Models.Responses;

/// <summary>
/// A Paystack transfer recipient, returned by create/bulk-create/list/fetch/update on the
/// Transfer Recipients API.
/// </summary>
public sealed class TransferRecipientData
{
    public bool Active { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public string? Currency { get; set; }

    public string? Domain { get; set; }

    /// <summary>Paystack's internal numeric recipient id.</summary>
    public long Id { get; set; }

    public long? Integration { get; set; }

    public string Name { get; set; } = string.Empty;

    /// <summary>Public identifier for the recipient, e.g. "RCP_xxx". Pass this as the <c>recipient</c> when initiating a transfer.</summary>
    public string RecipientCode { get; set; } = string.Empty;

    /// <summary>Recipient type. See <see cref="Common.PayStackTransferRecipientType"/>.</summary>
    public string Type { get; set; } = string.Empty;

    public DateTimeOffset? UpdatedAt { get; set; }

    /// <summary>Whether the recipient has been deactivated (via Delete Transfer Recipient).</summary>
    public bool IsDeleted { get; set; }

    public TransferRecipientDetails? Details { get; set; }

    public System.Text.Json.JsonElement? Metadata { get; set; }
}

/// <summary>Bank/authorization details embedded in a <see cref="TransferRecipientData"/>.</summary>
public sealed class TransferRecipientDetails
{
    public string? AuthorizationCode { get; set; }

    public string? AccountNumber { get; set; }

    public string? AccountName { get; set; }

    public string? BankCode { get; set; }

    public string? BankName { get; set; }
}

/// <summary>Result of <c>POST /transferrecipient/bulk</c>: recipients created successfully and any that failed.</summary>
public sealed class BulkCreateTransferRecipientData
{
    public List<TransferRecipientData> Success { get; set; } = [];

    public List<System.Text.Json.JsonElement> Errors { get; set; } = [];
}
