namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known values for a direct debit mandate authorization's <c>status</c>.
/// See <see cref="PayStackChannel"/> for the rationale on string constants over an enum.
/// </summary>
public static class PayStackMandateStatus
{
    public const string Pending = "pending";
    public const string Active = "active";
    public const string Revoked = "revoked";
}
