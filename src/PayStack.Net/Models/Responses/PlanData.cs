namespace PayStack.Net.Models.Responses;

/// <summary>
/// A Paystack subscription billing plan, returned by create/list/fetch plan endpoints.
/// </summary>
public sealed class PlanData
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    /// <summary>Public identifier for the plan, e.g. "PLN_xxx". Pass this as <c>plan</c> when creating a subscription.</summary>
    public string PlanCode { get; set; } = string.Empty;

    public string? Description { get; set; }

    /// <summary>Amount charged per interval, in the currency's subunit.</summary>
    public long Amount { get; set; }

    /// <summary>Billing interval, e.g. "monthly". See <see cref="Common.PayStackPlanInterval"/> for known values.</summary>
    public string Interval { get; set; } = string.Empty;

    public bool SendInvoices { get; set; }

    public bool SendSms { get; set; }

    public bool HostedPage { get; set; }

    public string? HostedPageUrl { get; set; }

    public string? HostedPageSummary { get; set; }

    public string Currency { get; set; } = string.Empty;

    public string? Domain { get; set; }

    public int Integration { get; set; }

    /// <summary>Subscriptions currently on this plan. Populated on list/fetch, not on create.</summary>
    public List<SubscriptionData>? Subscriptions { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}
