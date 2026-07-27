using System.Text.Json;

namespace PayStack.Net.Models.Responses;

/// <summary>
/// A Paystack customer, returned by create/list/update/set_risk_action. Not every field is
/// populated by every endpoint — e.g. <see cref="Integration"/>, <see cref="Identified"/>, and
/// <see cref="Identifications"/> are only returned by create; <see cref="FirstName"/>,
/// <see cref="LastName"/>, <see cref="Phone"/>, <see cref="Metadata"/>, and
/// <see cref="RiskAction"/> are only returned by list/update/set_risk_action. Use
/// <see cref="CustomerDetailData"/> for the richer shape returned by "fetch by email or code".
/// </summary>
public sealed class CustomerData
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

    /// <summary>Whether the customer's identity has been validated. Only returned by "create customer".</summary>
    public bool? Identified { get; set; }

    public JsonElement? Identifications { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}
