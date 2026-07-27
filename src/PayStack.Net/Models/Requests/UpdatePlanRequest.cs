namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>PUT /plan/:id_or_code</c>.</summary>
public sealed class UpdatePlanRequest
{
    /// <summary>Plan name. Required.</summary>
    public required string Name { get; set; }

    /// <summary>Amount to charge per interval, in the currency's subunit. Required.</summary>
    public required long Amount { get; set; }

    /// <summary>Billing interval. Use <see cref="Common.PayStackPlanInterval"/> constants. Required.</summary>
    public required string Interval { get; set; }

    public string? Description { get; set; }

    /// <summary>Whether to send an invoice notification email to the customer on each charge.</summary>
    public bool? SendInvoices { get; set; }

    /// <summary>Whether to send an SMS notification to the customer on each charge.</summary>
    public bool? SendSms { get; set; }

    public string? Currency { get; set; }

    public int? InvoiceLimit { get; set; }

    /// <summary>Whether to apply these changes to customers already subscribed to this plan. Defaults to <c>true</c>.</summary>
    public bool? UpdateExistingSubscriptions { get; set; }
}
