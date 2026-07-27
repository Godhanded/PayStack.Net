using System.Text.Json;
using PayStack.Net.Models.Common;

namespace PayStack.Net.Models.Responses;

/// <summary>
/// A Paystack subscription, returned by create/list/fetch subscription endpoints, and nested inside
/// <see cref="PlanData.Subscriptions"/>.
/// </summary>
public sealed class SubscriptionData
{
    public long Id { get; set; }

    /// <summary>
    /// The subscribed customer. On create/list this is the raw numeric customer id; on fetch (and
    /// sometimes list) Paystack expands this to a full customer object instead — exposed as a raw
    /// <see cref="JsonElement"/> since the shape varies. Use <see cref="JsonElement.ValueKind"/> to
    /// tell a plain id (<see cref="JsonValueKind.Number"/>) apart from an expanded object
    /// (<see cref="JsonValueKind.Object"/>).
    /// </summary>
    public JsonElement? Customer { get; set; }

    /// <summary>
    /// The subscription's plan. On create/list this is the raw numeric plan id; on fetch (and
    /// sometimes list) Paystack expands this to a full plan object instead — see <see cref="Customer"/>
    /// for how to tell the two shapes apart.
    /// </summary>
    public JsonElement? Plan { get; set; }

    public int Integration { get; set; }

    public AuthorizationData? Authorization { get; set; }

    public string? Domain { get; set; }

    /// <summary>Unix timestamp of the subscription's first charge.</summary>
    public long Start { get; set; }

    /// <summary>Subscription status, e.g. "active", "non-renewing", "cancelled". See <see cref="Common.PayStackSubscriptionStatus"/> for known values.</summary>
    public string Status { get; set; } = string.Empty;

    public int Quantity { get; set; }

    /// <summary>Amount charged per interval, in the currency's subunit.</summary>
    public long Amount { get; set; }

    /// <summary>Public identifier for the subscription, e.g. "SUB_xxx".</summary>
    public string SubscriptionCode { get; set; } = string.Empty;

    /// <summary>Token required alongside <see cref="SubscriptionCode"/> to enable/disable the subscription.</summary>
    public string EmailToken { get; set; } = string.Empty;

    public string? EasyCronId { get; set; }

    public string? CronExpression { get; set; }

    public DateTimeOffset? NextPaymentDate { get; set; }

    public string? OpenInvoice { get; set; }

    /// <summary>Invoice history for this subscription. Only populated on fetch-by-id.</summary>
    public List<JsonElement>? Invoices { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}
