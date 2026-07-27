using System.Text.Json;
using PayStack.Net.Models.Common;

namespace PayStack.Net.Models.Responses;

/// <summary>A single charge within a bulk charge batch, returned by <c>GET /bulkcharge/:id_or_code/charges</c>.</summary>
public sealed class BulkChargeItemData
{
    public long? Integration { get; set; }

    /// <summary>The numeric id of the parent bulk charge batch.</summary>
    public long Bulkcharge { get; set; }

    public CustomerSummary? Customer { get; set; }

    public AuthorizationData? Authorization { get; set; }

    /// <summary>
    /// The underlying transaction created for this charge. Not exposed as a strongly typed
    /// <see cref="TransactionData"/> because the research notes for this endpoint don't document
    /// its exact embedded shape — inspect the raw JSON if you need fields beyond what's typed elsewhere.
    /// </summary>
    public JsonElement? Transaction { get; set; }

    public string? Domain { get; set; }

    /// <summary>Amount charged, in the currency's subunit.</summary>
    public long Amount { get; set; }

    public string Currency { get; set; } = string.Empty;

    /// <summary>Charge status within the batch. See <see cref="Common.PayStackBulkChargeItemStatus"/>.</summary>
    public string Status { get; set; } = string.Empty;

    public long Id { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}
