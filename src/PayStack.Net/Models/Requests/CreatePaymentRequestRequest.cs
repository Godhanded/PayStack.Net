namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /paymentrequest</c>.</summary>
public sealed class CreatePaymentRequestRequest
{
    /// <summary>Customer id or code the request is for. Required.</summary>
    public required string Customer { get; set; }

    /// <summary>Amount to request, in the currency's subunit. Used when <see cref="LineItems"/>/<see cref="Tax"/> are not specified.</summary>
    public long? Amount { get; set; }

    /// <summary>When the request is due, in ISO 8601 format.</summary>
    public DateTimeOffset? DueDate { get; set; }

    public string? Description { get; set; }

    public List<PaymentRequestLineItemRequest>? LineItems { get; set; }

    public List<PaymentRequestTaxRequest>? Tax { get; set; }

    /// <summary>ISO 4217 currency code. Defaults to NGN.</summary>
    public string? Currency { get; set; }

    /// <summary>Whether to notify the customer. Defaults to <c>true</c>.</summary>
    public bool? SendNotification { get; set; }

    /// <summary>When <c>true</c>, saves the request without sending it; overrides <see cref="SendNotification"/>. Defaults to <c>false</c>.</summary>
    public bool? Draft { get; set; }

    public bool? HasInvoice { get; set; }

    public long? InvoiceNumber { get; set; }

    /// <summary>Split code of a pre-created transaction split to apply.</summary>
    public string? SplitCode { get; set; }
}

/// <summary>A single line item within a <see cref="CreatePaymentRequestRequest"/> or <see cref="UpdatePaymentRequestRequest"/>.</summary>
public sealed class PaymentRequestLineItemRequest
{
    public required string Name { get; set; }

    /// <summary>Line amount, in the currency's subunit.</summary>
    public required long Amount { get; set; }

    public int? Quantity { get; set; }
}

/// <summary>A single tax line within a <see cref="CreatePaymentRequestRequest"/> or <see cref="UpdatePaymentRequestRequest"/>.</summary>
public sealed class PaymentRequestTaxRequest
{
    public required string Name { get; set; }

    /// <summary>Tax amount, in the currency's subunit.</summary>
    public required long Amount { get; set; }
}
