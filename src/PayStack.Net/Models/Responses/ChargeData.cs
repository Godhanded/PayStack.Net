using System.Text.Json;
using PayStack.Net.Models.Common;

namespace PayStack.Net.Models.Responses;

/// <summary>
/// The result of a direct <see cref="Resources.IChargeClient"/> charge attempt — the same superset
/// shape as <see cref="TransactionData"/>. A charge can pause mid-flight awaiting more information
/// from the customer; branch on <see cref="Status"/> (see <see cref="PayStackChargeStatus"/>) to
/// decide whether to call one of the <c>submit_*</c> endpoints next or treat the charge as resolved.
/// </summary>
public sealed class ChargeData
{
    public ulong Id { get; set; }

    public string? Domain { get; set; }

    /// <summary>Charge status. See <see cref="PayStackChargeStatus"/> for known values, including intermediate "send_*" states.</summary>
    public string Status { get; set; } = string.Empty;

    public string Reference { get; set; } = string.Empty;

    /// <summary>Amount in the currency's subunit (e.g. kobo for NGN).</summary>
    public long Amount { get; set; }

    public string? Message { get; set; }

    /// <summary>Human-readable outcome from the payment processor, e.g. "Successful", "Declined".</summary>
    public string? GatewayResponse { get; set; }

    public DateTimeOffset? PaidAt { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public string? Channel { get; set; }

    public string Currency { get; set; } = string.Empty;

    public string? IpAddress { get; set; }

    /// <summary>Custom metadata, exposed as a raw <see cref="JsonElement"/>. See <see cref="TransactionData.Metadata"/> for the same convention.</summary>
    public JsonElement? Metadata { get; set; }

    public TransactionLog? Log { get; set; }

    /// <summary>Total fee charged for this charge, in subunits.</summary>
    public long? Fees { get; set; }

    public JsonElement? FeesSplit { get; set; }

    public JsonElement? FeesBreakdown { get; set; }

    public AuthorizationData? Authorization { get; set; }

    public CustomerSummary? Customer { get; set; }

    public JsonElement? Plan { get; set; }

    public JsonElement? PlanObject { get; set; }

    public JsonElement? Split { get; set; }

    public JsonElement? Subaccount { get; set; }

    public string? OrderId { get; set; }

    /// <summary>Amount originally requested before any partial payment/at_least adjustment, in subunits.</summary>
    public long? RequestedAmount { get; set; }

    public JsonElement? PosTransactionData { get; set; }

    public TransactionSource? Source { get; set; }

    public DateTimeOffset? TransactionDate { get; set; }

    /// <summary>
    /// Convenience accessor that returns <see cref="Metadata"/> deserialized as a
    /// <see cref="TransactionMetadata"/>, or <c>null</c> when no metadata object was set.
    /// </summary>
    public TransactionMetadata? GetMetadataObject()
    {
        if (Metadata is not { ValueKind: JsonValueKind.Object } element)
        {
            return null;
        }

        return element.Deserialize<TransactionMetadata>(PayStackJsonOptions.Default);
    }
}
