namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>PUT /paymentrequest/{id_or_code}</c>. All fields are optional — only the ones supplied are changed.</summary>
public sealed class UpdatePaymentRequestRequest
{
    public string? Customer { get; set; }

    /// <summary>Amount to request, in the currency's subunit.</summary>
    public long? Amount { get; set; }

    public DateTimeOffset? DueDate { get; set; }

    public string? Description { get; set; }

    public List<PaymentRequestLineItemRequest>? LineItems { get; set; }

    public List<PaymentRequestTaxRequest>? Tax { get; set; }

    public string? Currency { get; set; }

    public bool? SendNotification { get; set; }

    public bool? Draft { get; set; }

    public long? InvoiceNumber { get; set; }

    public string? SplitCode { get; set; }
}
