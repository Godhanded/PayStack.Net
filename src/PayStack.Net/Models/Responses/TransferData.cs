using System.Text.Json;
using PayStack.Net.Models.Common;

namespace PayStack.Net.Models.Responses;

/// <summary>
/// A Paystack transfer, returned by initiate/finalize/list/fetch/verify on the Transfers API.
/// Not every field is populated by every endpoint — <see cref="Recipient"/> is a bare recipient id
/// on Initiate Transfer but a full <see cref="TransferRecipientData"/> object on List/Fetch/Verify,
/// so it is exposed as a raw <see cref="JsonElement"/>; use <see cref="GetRecipientId"/> or
/// <see cref="GetRecipientObject"/> depending on which shape the calling endpoint returns.
/// </summary>
public sealed class TransferData
{
    public List<JsonElement>? TransferSessionId { get; set; }

    public List<JsonElement>? TransferTrials { get; set; }

    public string? Domain { get; set; }

    /// <summary>Amount transferred, in the currency's subunit.</summary>
    public long Amount { get; set; }

    public string Currency { get; set; } = string.Empty;

    public string Reference { get; set; } = string.Empty;

    /// <summary>Funding source. Currently always "balance".</summary>
    public string? Source { get; set; }

    public JsonElement? SourceDetails { get; set; }

    public string? Reason { get; set; }

    /// <summary>Transfer status, e.g. "pending", "otp", "success", "failed", "reversed". See <see cref="Common.PayStackTransferStatus"/>.</summary>
    public string Status { get; set; } = string.Empty;

    public JsonElement? Failures { get; set; }

    /// <summary>Public identifier for the transfer, e.g. "TRF_xxx".</summary>
    public string TransferCode { get; set; } = string.Empty;

    public string? TitanCode { get; set; }

    public DateTimeOffset? TransferredAt { get; set; }

    /// <summary>Paystack's internal numeric transfer id.</summary>
    public long Id { get; set; }

    public long? Integration { get; set; }

    public JsonElement? Request { get; set; }

    /// <summary>The recipient. A bare numeric id on Initiate Transfer; a full object on List/Fetch/Verify. See the type's summary.</summary>
    public JsonElement? Recipient { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    /// <summary>Fetch/Verify only: session provider and id used to process the transfer.</summary>
    public TransferSession? Session { get; set; }

    /// <summary>Fetch/Verify only: fee charged for this transfer, in subunits.</summary>
    public long? FeeCharged { get; set; }

    public JsonElement? FeesBreakdown { get; set; }

    /// <summary>Fetch/Verify only: human-readable outcome from the payment processor.</summary>
    public string? GatewayResponse { get; set; }

    /// <summary>
    /// Convenience accessor for the common case where <see cref="Recipient"/> is a bare numeric id
    /// (as returned by Initiate Transfer). Returns <c>null</c> if <see cref="Recipient"/> is an object instead.
    /// </summary>
    public long? GetRecipientId()
    {
        if (Recipient is { ValueKind: JsonValueKind.Number } element)
        {
            return element.GetInt64();
        }

        return null;
    }

    /// <summary>
    /// Convenience accessor for the common case where <see cref="Recipient"/> is a full recipient
    /// object (as returned by List/Fetch/Verify Transfer). Returns <c>null</c> if <see cref="Recipient"/>
    /// is a bare id instead.
    /// </summary>
    public TransferRecipientData? GetRecipientObject()
    {
        if (Recipient is not { ValueKind: JsonValueKind.Object } element)
        {
            return null;
        }

        return element.Deserialize<TransferRecipientData>(PayStackJsonOptions.Default);
    }
}

/// <summary>Session details for a completed transfer, embedded in <see cref="TransferData"/> on Fetch/Verify.</summary>
public sealed class TransferSession
{
    public string? Provider { get; set; }

    public string? Id { get; set; }
}

/// <summary>Result of a single transfer within a <c>POST /transfer/bulk</c> request.</summary>
public sealed class BulkTransferResultData
{
    public string Reference { get; set; } = string.Empty;

    public string Recipient { get; set; } = string.Empty;

    public long Amount { get; set; }

    public string TransferCode { get; set; } = string.Empty;

    public string Currency { get; set; } = string.Empty;

    /// <summary>Transfer status. See <see cref="Common.PayStackTransferStatus"/>.</summary>
    public string Status { get; set; } = string.Empty;
}
