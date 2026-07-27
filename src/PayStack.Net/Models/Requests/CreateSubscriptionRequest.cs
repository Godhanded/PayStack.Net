namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /subscription</c>.</summary>
public sealed class CreateSubscriptionRequest
{
    /// <summary>Customer's email address or customer code to subscribe. Required.</summary>
    public required string Customer { get; set; }

    /// <summary>Plan code to subscribe the customer to. Required.</summary>
    public required string Plan { get; set; }

    /// <summary>
    /// A specific reusable authorization code to charge for this subscription. If omitted, Paystack
    /// uses the customer's most recent authorization on the plan's currency.
    /// </summary>
    public string? Authorization { get; set; }

    /// <summary>
    /// ISO 8601 timestamp for when the first charge should occur. Set this in the future to model a
    /// free trial period — Paystack does not have a dedicated "trial" endpoint; delaying the first
    /// charge via this field (or via the plan's <c>invoice_limit</c>/<c>send_invoices</c> settings) is
    /// the documented way to achieve one.
    /// </summary>
    public string? StartDate { get; set; }
}
