using System.Text.Json;

namespace PayStack.Net.Models.Responses;

/// <summary>A transaction split attached to a virtual terminal, returned by the add-split-code endpoint.</summary>
public sealed class VirtualTerminalSplitData
{
    public long Id { get; set; }

    public string? Name { get; set; }

    /// <summary>Split type, e.g. "percentage", "flat".</summary>
    public string? Type { get; set; }

    public string? Currency { get; set; }

    public long? Integration { get; set; }

    public string? Domain { get; set; }

    public string SplitCode { get; set; } = string.Empty;

    public bool Active { get; set; }

    /// <summary>Who bears the Paystack fee: "account" or "subaccount".</summary>
    public string? BearerType { get; set; }

    public JsonElement? BearerSubaccount { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public bool IsDynamic { get; set; }

    public List<VirtualTerminalSplitSubaccount>? Subaccounts { get; set; }

    public int? TotalSubaccounts { get; set; }
}

/// <summary>A subaccount's share within a transaction split.</summary>
public sealed class VirtualTerminalSplitSubaccount
{
    public JsonElement? Subaccount { get; set; }

    /// <summary>The subaccount's share of the split, as a fraction or flat amount depending on <see cref="VirtualTerminalSplitData.Type"/>.</summary>
    public double? Share { get; set; }
}
