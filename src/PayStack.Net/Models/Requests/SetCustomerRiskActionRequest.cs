namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /customer/set_risk_action</c> — whitelists or blacklists a customer.</summary>
public sealed class SetCustomerRiskActionRequest
{
    /// <summary>The customer's email or customer code. Required.</summary>
    public required string Customer { get; set; }

    /// <summary>Use <see cref="Common.PayStackRiskAction"/> constants. Defaults to "default" when omitted.</summary>
    public string? RiskAction { get; set; }
}
