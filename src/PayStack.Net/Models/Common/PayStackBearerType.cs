namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known values for who bears the Paystack transaction fee on a split payment.
/// See <see cref="PayStackChannel"/> for the rationale on string constants over an enum.
/// </summary>
public static class PayStackBearerType
{
    /// <summary>A single designated subaccount bears the fee.</summary>
    public const string Subaccount = "subaccount";

    /// <summary>The main account bears the fee.</summary>
    public const string Account = "account";

    /// <summary>The fee is shared proportionally across all subaccounts in the split.</summary>
    public const string AllProportional = "all-proportional";

    /// <summary>The fee is shared equally across all subaccounts in the split.</summary>
    public const string All = "all";
}
