namespace PayStack.Net.Models.Common;

/// <summary>The abbreviated customer object embedded in transaction, subscription, and dispute payloads.</summary>
public sealed class CustomerSummary
{
    /// <summary>Paystack's internal numeric customer id.</summary>
    public long Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Email { get; set; } = string.Empty;

    /// <summary>Public identifier for the customer, e.g. "CUS_xxx".</summary>
    public string CustomerCode { get; set; } = string.Empty;

    public string? Phone { get; set; }

    /// <summary>Arbitrary metadata previously attached to the customer, or <c>null</c>.</summary>
    public System.Text.Json.JsonElement? Metadata { get; set; }

    /// <summary>Risk classification, e.g. "default", "allow", "deny".</summary>
    public string? RiskAction { get; set; }

    public string? InternationalFormatPhone { get; set; }
}
