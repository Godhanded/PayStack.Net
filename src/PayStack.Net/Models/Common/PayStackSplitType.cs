namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known values for a transaction split's <c>type</c>: how each subaccount's <c>share</c> is
/// interpreted. See <see cref="PayStackChannel"/> for the rationale on string constants over an enum.
/// </summary>
public static class PayStackSplitType
{
    /// <summary>Each subaccount's share is a percentage of the transaction amount.</summary>
    public const string Percentage = "percentage";

    /// <summary>Each subaccount's share is a flat amount, in subunits.</summary>
    public const string Flat = "flat";
}
