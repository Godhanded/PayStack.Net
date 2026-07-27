namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known values for a customer's <c>risk_action</c> (whitelist/blacklist classification).
/// See <see cref="PayStackChannel"/> for the rationale on string constants over an enum.
/// </summary>
public static class PayStackRiskAction
{
    /// <summary>No override; risk is assessed normally.</summary>
    public const string Default = "default";

    /// <summary>Whitelisted — the customer's charges bypass normal risk checks.</summary>
    public const string Allow = "allow";

    /// <summary>Blacklisted — the customer's charges are blocked.</summary>
    public const string Deny = "deny";
}
