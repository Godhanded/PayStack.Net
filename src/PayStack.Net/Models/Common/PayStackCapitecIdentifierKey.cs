namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known values for <see cref="Requests.ChargeCapitecPayDetails.IdentifierKey"/>.
/// See <see cref="PayStackChannel"/> for the rationale on string constants over an enum.
/// </summary>
public static class PayStackCapitecIdentifierKey
{
    public const string Cellphone = "CELLPHONE";
    public const string IdNumber = "IDNUMBER";
    public const string AccountNumber = "ACCOUNTNUMBER";
}
