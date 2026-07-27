using System.Text.Json;
using PayStack.Net.Models.Common;

namespace PayStack.Net.Models.Responses;

/// <summary>
/// A Paystack transaction, returned by initialize/verify/list/fetch/charge_authorization/partial_debit.
/// Not every field is populated by every endpoint — see the calling method's XML doc for specifics
/// (e.g. <see cref="HelpdeskLink"/> is only returned by "fetch by id").
/// </summary>
public sealed class TransactionData
{
    /// <summary>Paystack's internal numeric transaction id. Treat as unsigned — Paystack ids can exceed <see cref="long.MaxValue"/> for very active accounts.</summary>
    public ulong Id { get; set; }

    public string? Domain { get; set; }

    /// <summary>Transaction status, e.g. "success", "failed", "abandoned", "reversed". See <see cref="Common.PayStackTransactionStatus"/> for known values.</summary>
    public string Status { get; set; } = string.Empty;

    public string Reference { get; set; } = string.Empty;

    public string? ReceiptNumber { get; set; }

    /// <summary>Amount in the currency's subunit (e.g. kobo for NGN).</summary>
    public long Amount { get; set; }

    public string? Message { get; set; }

    /// <summary>Human-readable outcome from the payment processor, e.g. "Successful", "Declined".</summary>
    public string? GatewayResponse { get; set; }

    /// <summary>Only present when fetching a single transaction by id.</summary>
    public string? HelpdeskLink { get; set; }

    public DateTimeOffset? PaidAt { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public string? Channel { get; set; }

    public string Currency { get; set; } = string.Empty;

    public string? IpAddress { get; set; }

    /// <summary>
    /// Custom metadata attached at initialization. Paystack returns either an empty string or an
    /// object, so this is exposed as a raw <see cref="JsonElement"/> — use
    /// <see cref="JsonElement.ValueKind"/> to check before deserializing further, or call
    /// <see cref="GetMetadataObject"/> for the common case.
    /// </summary>
    public JsonElement? Metadata { get; set; }

    public TransactionLog? Log { get; set; }

    /// <summary>Total fee charged for this transaction, in subunits.</summary>
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

    public JsonElement? Connect { get; set; }

    public DateTimeOffset? TransactionDate { get; set; }

    /// <summary>
    /// Convenience accessor that returns <see cref="Metadata"/> deserialized as an
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

/// <summary>Origin details for a transaction (e.g. initiated from an API call vs. a payment page).</summary>
public sealed class TransactionSource
{
    public string? Source { get; set; }

    public string? Type { get; set; }

    public string? Identifier { get; set; }

    public string? EntryPoint { get; set; }
}

/// <summary>Strongly typed shape of the common "custom_fields" metadata convention used across the API.</summary>
public sealed class TransactionMetadata
{
    public List<TransactionCustomField>? CustomFields { get; set; }

    public string? CancelAction { get; set; }
}

public sealed class TransactionCustomField
{
    public string DisplayName { get; set; } = string.Empty;

    public string VariableName { get; set; } = string.Empty;

    public string? Value { get; set; }
}
