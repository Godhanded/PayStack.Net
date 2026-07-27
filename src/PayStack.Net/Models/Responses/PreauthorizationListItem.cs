using PayStack.Net.Models.Common;

namespace PayStack.Net.Models.Responses;

/// <summary>A single row returned by <c>GET /preauthorization</c>.</summary>
public sealed class PreauthorizationListItem
{
    public long Id { get; set; }

    public string? Domain { get; set; }

    /// <summary>See <see cref="Common.PayStackPreauthorizationStatus"/> for known values.</summary>
    public string Status { get; set; } = string.Empty;

    public string Reference { get; set; } = string.Empty;

    /// <summary>Amount in the currency's subunit.</summary>
    public long Amount { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>Id of the underlying capture transaction, once captured. Treat as unsigned — can exceed <see cref="long.MaxValue"/> per Paystack's transaction id convention.</summary>
    public ulong? TransactionId { get; set; }

    public DateTimeOffset? CapturedAt { get; set; }

    public DateTimeOffset? ReleasedAt { get; set; }

    public string Currency { get; set; } = string.Empty;

    public long? Fees { get; set; }

    public AuthorizationData? Authorization { get; set; }

    public CustomerSummary? Customer { get; set; }
}
