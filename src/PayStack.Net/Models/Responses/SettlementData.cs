using System.Text.Json;

namespace PayStack.Net.Models.Responses;

/// <summary>A settlement paid out to one of your settlement accounts, returned by <c>GET /settlement</c>.</summary>
public sealed class SettlementData
{
    /// <summary>Paystack's internal numeric settlement id.</summary>
    public long Id { get; set; }

    /// <summary>"live" or "test".</summary>
    public string? Domain { get; set; }

    /// <summary>Settlement status, e.g. "success", "pending". See <see cref="Common.PayStackSettlementStatus"/>.</summary>
    public string Status { get; set; } = string.Empty;

    public string Currency { get; set; } = string.Empty;

    public long Integration { get; set; }

    /// <summary>Gross amount settled, in the currency's subunit.</summary>
    public long TotalAmount { get; set; }

    /// <summary>Net amount actually paid out, in the currency's subunit (after fees/deductions).</summary>
    public long EffectiveAmount { get; set; }

    /// <summary>Total Paystack fees deducted, in the currency's subunit.</summary>
    public long TotalFees { get; set; }

    /// <summary>Number of transactions included in this settlement.</summary>
    public long TotalProcessed { get; set; }

    /// <summary>Additional deductions applied to this settlement, if any. Shape is not fixed, exposed as raw JSON.</summary>
    public JsonElement? Deductions { get; set; }

    public DateTimeOffset? SettlementDate { get; set; }

    public string? SettledBy { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}
