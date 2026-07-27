using System.Text.Json;
using PayStack.Net.Models.Common;

namespace PayStack.Net.Models.Responses;

/// <summary>
/// A Paystack dispute, returned by list/fetch/list-by-transaction/update/resolve on the Disputes API.
/// Not every field is populated by every endpoint — e.g. <see cref="Message"/> is only populated by
/// the resolve endpoint, and <see cref="MerchantTransactionReference"/> only by the
/// list-by-transaction endpoint.
/// </summary>
public sealed class DisputeData
{
    /// <summary>Paystack's internal numeric dispute id.</summary>
    public long Id { get; set; }

    public long? Integration { get; set; }

    /// <summary>Amount to be refunded, in the currency's subunit.</summary>
    public long? RefundAmount { get; set; }

    public string? Currency { get; set; }

    /// <summary>Dispute status, e.g. "pending", "resolved". See <see cref="PayStackDisputeStatus"/> for known values.</summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>Outcome of a resolved dispute, e.g. "merchant-accepted", "declined". See <see cref="PayStackDisputeResolution"/>.</summary>
    public string? Resolution { get; set; }

    public string? Domain { get; set; }

    /// <summary>The disputed transaction. Only populated when the endpoint expands it (e.g. list/fetch).</summary>
    public TransactionData? Transaction { get; set; }

    public string? TransactionReference { get; set; }

    /// <summary>Merchant-assigned transaction reference. Only populated by the list-by-transaction endpoint.</summary>
    public string? MerchantTransactionReference { get; set; }

    public string? Category { get; set; }

    public CustomerSummary? Customer { get; set; }

    /// <summary>First 6 digits of the disputed card, when available.</summary>
    public string? Bin { get; set; }

    /// <summary>Last 4 digits of the disputed card, when available.</summary>
    public string? Last4 { get; set; }

    public DateTimeOffset? DueAt { get; set; }

    public DateTimeOffset? ResolvedAt { get; set; }

    /// <summary>Evidence attached to the dispute. Shape varies by endpoint, so exposed as a raw <see cref="JsonElement"/>.</summary>
    public JsonElement? Evidence { get; set; }

    public string? Attachments { get; set; }

    public string? Note { get; set; }

    public List<DisputeHistoryEntry>? History { get; set; }

    public List<DisputeMessage>? Messages { get; set; }

    public string? CreatedBy { get; set; }

    /// <summary>The resolution message. Only populated by <c>ResolveAsync</c>.</summary>
    public DisputeMessage? Message { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}

/// <summary>A single status transition in a dispute's history.</summary>
public sealed class DisputeHistoryEntry
{
    public long? Id { get; set; }

    /// <summary>The dispute id this entry belongs to. Only populated by the list-by-transaction endpoint.</summary>
    public long? Dispute { get; set; }

    public string Status { get; set; } = string.Empty;

    public string? By { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}

/// <summary>A message exchanged as part of a dispute's conversation thread, or its resolution message.</summary>
public sealed class DisputeMessage
{
    public long? Id { get; set; }

    /// <summary>The dispute id this message belongs to. Only populated by the list-by-transaction endpoint.</summary>
    public long? Dispute { get; set; }

    public string? Sender { get; set; }

    public string? Body { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}
