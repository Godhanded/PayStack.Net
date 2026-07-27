namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /transferrecipient/bulk</c>.</summary>
public sealed class BulkCreateTransferRecipientRequest
{
    /// <summary>Recipients to create. Each entry requires at minimum <see cref="TransferRecipientBatchItem.Type"/>, <see cref="TransferRecipientBatchItem.Name"/>, and <see cref="TransferRecipientBatchItem.BankCode"/>.</summary>
    public required List<TransferRecipientBatchItem> Batch { get; set; }
}

/// <summary>A single recipient entry within a <see cref="BulkCreateTransferRecipientRequest"/>.</summary>
public sealed class TransferRecipientBatchItem
{
    /// <summary>Recipient type. Use <see cref="Common.PayStackTransferRecipientType"/> constants. Required.</summary>
    public required string Type { get; set; }

    /// <summary>Recipient's name. Required.</summary>
    public required string Name { get; set; }

    /// <summary>Bank code, obtained from the List Banks endpoint. Required.</summary>
    public required string BankCode { get; set; }

    public string? AccountNumber { get; set; }

    public string? Description { get; set; }

    public string? Currency { get; set; }

    public string? AuthorizationCode { get; set; }

    public object? Metadata { get; set; }
}
