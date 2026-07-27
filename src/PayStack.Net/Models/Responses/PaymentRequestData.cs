using System.Text.Json;
using PayStack.Net.Models.Common;

namespace PayStack.Net.Models.Responses;

/// <summary>
/// A Paystack payment request (invoice), returned by create/list/fetch/verify/finalize/update.
/// Not every field is populated by every endpoint — e.g. <see cref="Transactions"/> is only
/// populated by "fetch", and <see cref="Integration"/> only by "verify".
/// </summary>
public sealed class PaymentRequestData
{
    /// <summary>Paystack's internal numeric payment request id.</summary>
    public long Id { get; set; }

    public string? Domain { get; set; }

    /// <summary>Amount to collect, in the currency's subunit.</summary>
    public long Amount { get; set; }

    public string Currency { get; set; } = string.Empty;

    public DateTimeOffset? DueDate { get; set; }

    public bool HasInvoice { get; set; }

    public long? InvoiceNumber { get; set; }

    public string? Description { get; set; }

    /// <summary>Signed, time-limited URL to a PDF rendition of the invoice.</summary>
    public string? PdfUrl { get; set; }

    public List<PaymentRequestLineItem>? LineItems { get; set; }

    public List<PaymentRequestTax>? Tax { get; set; }

    /// <summary>Public identifier for the payment request, e.g. "PRQ_xxx".</summary>
    public string RequestCode { get; set; } = string.Empty;

    /// <summary>Status, e.g. "pending", "success". See <see cref="PayStackPaymentRequestStatus"/>.</summary>
    public string Status { get; set; } = string.Empty;

    public bool Paid { get; set; }

    public DateTimeOffset? PaidAt { get; set; }

    public JsonElement? Metadata { get; set; }

    /// <summary>Notification delivery attempts for this payment request. Shape is not fixed, exposed as raw JSON.</summary>
    public List<JsonElement>? Notifications { get; set; }

    public string? OfflineReference { get; set; }

    /// <summary>Discount applied to the request. Paystack's docs do not confirm whether this is a subunit amount or a percentage.</summary>
    public decimal? Discount { get; set; }

    public bool Archived { get; set; }

    public string? Source { get; set; }

    public string? PaymentMethod { get; set; }

    public string? Note { get; set; }

    /// <summary>Amount already paid towards this request, in the currency's subunit.</summary>
    public long? AmountPaid { get; set; }

    /// <summary>Amount still outstanding, in the currency's subunit.</summary>
    public long? PendingAmount { get; set; }

    /// <summary>
    /// The customer this request is for. Paystack returns either a bare numeric customer id (on create)
    /// or an expanded customer object (on list/fetch/verify), so this is exposed as raw
    /// <see cref="JsonElement"/> — use <see cref="GetCustomerObject"/> for the expanded case.
    /// </summary>
    public JsonElement? Customer { get; set; }

    /// <summary>
    /// Only populated by "verify". Exposed as raw JSON since it is not present on other endpoints;
    /// use <see cref="GetIntegrationObject"/> to deserialize it.
    /// </summary>
    public JsonElement? Integration { get; set; }

    /// <summary>Transactions made against this payment request. Only populated by "fetch". Shape is not fixed, exposed as raw JSON.</summary>
    public List<JsonElement>? Transactions { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    /// <summary>
    /// Convenience accessor that returns <see cref="Customer"/> deserialized as an expanded
    /// <see cref="PaymentRequestCustomer"/>, or <c>null</c> when <see cref="Customer"/> is a bare id or absent.
    /// </summary>
    public PaymentRequestCustomer? GetCustomerObject()
    {
        if (Customer is not { ValueKind: JsonValueKind.Object } element)
        {
            return null;
        }

        return element.Deserialize<PaymentRequestCustomer>(PayStackJsonOptions.Default);
    }

    /// <summary>
    /// Convenience accessor that returns <see cref="Integration"/> deserialized as a
    /// <see cref="PaymentRequestIntegration"/>, or <c>null</c> when absent.
    /// </summary>
    public PaymentRequestIntegration? GetIntegrationObject()
    {
        if (Integration is not { ValueKind: JsonValueKind.Object } element)
        {
            return null;
        }

        return element.Deserialize<PaymentRequestIntegration>(PayStackJsonOptions.Default);
    }
}

/// <summary>A single line item within a payment request.</summary>
public sealed class PaymentRequestLineItem
{
    public string Name { get; set; } = string.Empty;

    /// <summary>Line amount, in the currency's subunit.</summary>
    public long Amount { get; set; }

    public int? Quantity { get; set; }
}

/// <summary>A single tax line within a payment request.</summary>
public sealed class PaymentRequestTax
{
    public string Name { get; set; } = string.Empty;

    /// <summary>Tax amount, in the currency's subunit.</summary>
    public long Amount { get; set; }
}

/// <summary>The expanded customer object embedded in a payment request on list/fetch/verify.</summary>
public sealed class PaymentRequestCustomer
{
    public long Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Email { get; set; } = string.Empty;

    public string CustomerCode { get; set; } = string.Empty;

    public string? Phone { get; set; }

    public JsonElement? Metadata { get; set; }

    public string? RiskAction { get; set; }

    public string? InternationalFormatPhone { get; set; }

    /// <summary>Only populated by "fetch". Shape is not fixed, exposed as raw JSON.</summary>
    public List<JsonElement>? Transactions { get; set; }

    /// <summary>Only populated by "fetch". Shape is not fixed, exposed as raw JSON.</summary>
    public List<JsonElement>? Subscriptions { get; set; }

    /// <summary>Only populated by "fetch". Shape is not fixed, exposed as raw JSON.</summary>
    public List<JsonElement>? Authorizations { get; set; }
}

/// <summary>Integration details returned when verifying a payment request.</summary>
public sealed class PaymentRequestIntegration
{
    public string? Key { get; set; }

    public string? Name { get; set; }

    public string? Logo { get; set; }

    public List<string>? AllowedCurrencies { get; set; }
}
