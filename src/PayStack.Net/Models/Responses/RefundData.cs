using System.Text.Json;
using PayStack.Net.Models.Common;

namespace PayStack.Net.Models.Responses;

/// <summary>
/// A Paystack refund, returned by create/retry/list/fetch on the Refunds API.
/// Not every field is populated by every endpoint. Notably <see cref="Transaction"/> is the full
/// nested transaction object on the create response but just a numeric transaction id elsewhere,
/// so it is exposed as a raw <see cref="JsonElement"/>.
/// </summary>
public sealed class RefundData
{
    public long Id { get; set; }

    public long? Integration { get; set; }

    /// <summary>
    /// The refunded transaction. A full nested transaction object on the create response; just a
    /// numeric transaction id on retry/list/fetch responses.
    /// </summary>
    public JsonElement? Transaction { get; set; }

    public JsonElement? Dispute { get; set; }

    public JsonElement? Settlement { get; set; }

    public string? Domain { get; set; }

    public string Currency { get; set; } = string.Empty;

    /// <summary>Amount requested to be refunded, in the currency's subunit.</summary>
    public long Amount { get; set; }

    /// <summary>Amount actually deducted from the merchant balance, in the currency's subunit.</summary>
    public long? DeductedAmount { get; set; }

    public bool? FullyDeducted { get; set; }

    public string? Channel { get; set; }

    /// <summary>Refund status, e.g. "pending", "processed". See <see cref="PayStackRefundStatus"/> for known values.</summary>
    public string Status { get; set; } = string.Empty;

    public string? RefundedBy { get; set; }

    public DateTimeOffset? RefundedAt { get; set; }

    public DateTimeOffset? ExpectedAt { get; set; }

    public string? CustomerNote { get; set; }

    public string? MerchantNote { get; set; }

    /// <summary>Bank reference for the refund, when applicable. Only populated by <c>RetryAsync</c>.</summary>
    public string? BankReference { get; set; }

    /// <summary>Failure/retry reason, when applicable. Only populated by <c>RetryAsync</c>.</summary>
    public string? Reason { get; set; }

    public JsonElement? Customer { get; set; }

    public string? InitiatedBy { get; set; }

    public DateTimeOffset? ReversedAt { get; set; }

    public string? SessionId { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}
