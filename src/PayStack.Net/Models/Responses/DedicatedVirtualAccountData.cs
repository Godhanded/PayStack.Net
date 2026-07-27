using System.Text.Json;
using PayStack.Net.Models.Common;

namespace PayStack.Net.Models.Responses;

/// <summary>A dedicated virtual (NUBAN/GHIPSS) account assigned to a customer for receiving bank transfers.</summary>
public sealed class DedicatedVirtualAccountData
{
    /// <summary>Paystack's internal numeric id for this dedicated account.</summary>
    public long Id { get; set; }

    public DedicatedVirtualAccountBank? Bank { get; set; }

    public string? AccountName { get; set; }

    public string? AccountNumber { get; set; }

    /// <summary>Whether the account has been assigned to a customer.</summary>
    public bool Assigned { get; set; }

    public string? Currency { get; set; }

    public JsonElement? Metadata { get; set; }

    public bool Active { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DedicatedVirtualAccountAssignment? Assignment { get; set; }

    public CustomerSummary? Customer { get; set; }

    /// <summary>Split configuration applied to transactions on this dedicated account, when one is set.</summary>
    public JsonElement? SplitConfig { get; set; }
}

/// <summary>The settlement bank underlying a dedicated virtual account.</summary>
public sealed class DedicatedVirtualAccountBank
{
    public string? Name { get; set; }

    public long Id { get; set; }

    public string? Slug { get; set; }
}

/// <summary>Details about how/when a dedicated virtual account was assigned.</summary>
public sealed class DedicatedVirtualAccountAssignment
{
    public string? Integration { get; set; }

    public string? AssigneeId { get; set; }

    public string? AssigneeType { get; set; }

    public bool Expired { get; set; }

    public string? AccountType { get; set; }

    public DateTimeOffset? AssignedAt { get; set; }
}
