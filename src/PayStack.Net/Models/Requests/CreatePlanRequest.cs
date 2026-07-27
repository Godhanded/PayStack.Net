namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /plan</c>.</summary>
public sealed class CreatePlanRequest
{
    /// <summary>Plan name. Required.</summary>
    public required string Name { get; set; }

    /// <summary>Amount to charge per interval, in the currency's subunit. Required.</summary>
    public required long Amount { get; set; }

    /// <summary>Billing interval. Use <see cref="Common.PayStackPlanInterval"/> constants. Required. Note: "hourly" is only accepted on update, not create.</summary>
    public required string Interval { get; set; }

    public string? Description { get; set; }

    /// <summary>Whether to send an invoice notification email to the customer on each charge.</summary>
    public bool? SendInvoices { get; set; }

    /// <summary>Whether to send an SMS notification to the customer on each charge.</summary>
    public bool? SendSms { get; set; }

    /// <summary>ISO 4217 currency code. Defaults to your integration's default currency.</summary>
    public string? Currency { get; set; }

    /// <summary>Number of invoices to raise during the subscription before it stops. Omit for an unlimited/ongoing plan.</summary>
    public int? InvoiceLimit { get; set; }
}
