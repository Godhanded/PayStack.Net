using System.Text.Json;
using PayStack.Net.Models.Common;

namespace PayStack.Net.Models.Responses;

/// <summary>
/// The full customer detail returned by <c>GET /customer/:email_or_code</c> — the base customer
/// fields plus their transaction/subscription/authorization history.
/// </summary>
public sealed class CustomerDetailData
{
    public long Id { get; set; }

    public long? Integration { get; set; }

    public string? Domain { get; set; }

    public string CustomerCode { get; set; } = string.Empty;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Email { get; set; } = string.Empty;

    public string? Phone { get; set; }

    public JsonElement? Metadata { get; set; }

    /// <summary>Risk classification. See <see cref="Common.PayStackRiskAction"/> for known values.</summary>
    public string? RiskAction { get; set; }

    public bool? Identified { get; set; }

    public JsonElement? Identifications { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public List<TransactionData>? Transactions { get; set; }

    /// <summary>Raw subscription objects; typed loosely to avoid a hard dependency on the Subscriptions resource's DTOs.</summary>
    public List<JsonElement>? Subscriptions { get; set; }

    public List<AuthorizationData>? Authorizations { get; set; }

    public int? TotalTransactions { get; set; }

    /// <summary>Total transaction value, broken down per currency.</summary>
    public JsonElement? TotalTransactionValue { get; set; }

    /// <summary>Raw dedicated virtual account object, when one is assigned; typed loosely to avoid a hard dependency on the Dedicated Virtual Accounts resource's DTOs.</summary>
    public JsonElement? DedicatedAccount { get; set; }
}
