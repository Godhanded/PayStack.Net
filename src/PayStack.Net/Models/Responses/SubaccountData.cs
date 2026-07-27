using System.Text.Json;

namespace PayStack.Net.Models.Responses;

/// <summary>
/// A Paystack subaccount, used to split transaction proceeds with a business/vendor.
/// Not every field is populated by every endpoint — list responses return a lighter shape than
/// create/fetch/update.
/// </summary>
public sealed class SubaccountData
{
    public long Id { get; set; }

    public string? SubaccountCode { get; set; }

    public string? BusinessName { get; set; }

    public string? Description { get; set; }

    public string? PrimaryContactName { get; set; }

    public string? PrimaryContactEmail { get; set; }

    public string? PrimaryContactPhone { get; set; }

    public JsonElement? Metadata { get; set; }

    /// <summary>Percentage of each transaction routed to this subaccount.</summary>
    public double PercentageCharge { get; set; }

    public string? SettlementBank { get; set; }

    /// <summary>Present on the list-shape response; the numeric id of <see cref="SettlementBank"/>.</summary>
    public long? BankId { get; set; }

    /// <summary>Present on the full (create/fetch/update) response shape.</summary>
    public BankSummary? Bank { get; set; }

    public string? AccountNumber { get; set; }

    public string? Currency { get; set; }

    public long? Integration { get; set; }

    public string? Domain { get; set; }

    public string? AccountName { get; set; }

    public string? Product { get; set; }

    public bool? ManagedByIntegration { get; set; }

    public bool? IsVerified { get; set; }

    /// <summary>How often this subaccount is settled, e.g. "auto", "weekly", "monthly", "manual". See <see cref="Common.PayStackSettlementSchedule"/>.</summary>
    public string? SettlementSchedule { get; set; }

    /// <summary>Whether the subaccount is active. On the list-shape response this may arrive as an integer (0/1) rather than a bool.</summary>
    public bool? Active { get; set; }

    public bool? Migrate { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}

/// <summary>Minimal bank identity embedded in the full subaccount response shape.</summary>
public sealed class BankSummary
{
    public long Id { get; set; }
}
