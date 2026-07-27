using System.Text.Json;

namespace PayStack.Net.Models.Responses;

/// <summary>A Paystack transaction split, returned by the create/list/fetch/update/subaccount-add split endpoints.</summary>
public sealed class SplitData
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    /// <summary>How subaccount shares are interpreted. See <see cref="Common.PayStackSplitType"/> for known values.</summary>
    public string Type { get; set; } = string.Empty;

    public string Currency { get; set; } = string.Empty;

    public long Integration { get; set; }

    public string? Domain { get; set; }

    public string SplitCode { get; set; } = string.Empty;

    public bool Active { get; set; }

    /// <summary>Who bears the Paystack fee. See <see cref="Common.PayStackBearerType"/> for known values.</summary>
    public string? BearerType { get; set; }

    public string? BearerSubaccount { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public bool IsDynamic { get; set; }

    public List<SplitSubaccountAllocation>? Subaccounts { get; set; }

    public int TotalSubaccounts { get; set; }
}

/// <summary>A subaccount's participation in a <see cref="SplitData"/>.</summary>
public sealed class SplitSubaccountAllocation
{
    public SplitSubaccountSummary? Subaccount { get; set; }

    /// <summary>This subaccount's share, interpreted per the split's <see cref="SplitData.Type"/>.</summary>
    public int Share { get; set; }
}

/// <summary>The abbreviated subaccount object embedded in a <see cref="SplitSubaccountAllocation"/>.</summary>
public sealed class SplitSubaccountSummary
{
    public long Id { get; set; }

    public string SubaccountCode { get; set; } = string.Empty;

    public string? BusinessName { get; set; }

    public string? Description { get; set; }

    public string? PrimaryContactName { get; set; }

    public string? PrimaryContactEmail { get; set; }

    public string? PrimaryContactPhone { get; set; }

    public JsonElement? Metadata { get; set; }

    public string? SettlementBank { get; set; }

    public string? Currency { get; set; }

    public string? AccountNumber { get; set; }
}
