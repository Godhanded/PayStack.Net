namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known document type values accepted by the account validation endpoint.
/// Modeled as string constants rather than a C# <c>enum</c> so the SDK stays forward-compatible
/// when Paystack adds a new value.
/// </summary>
public static class PayStackDocumentType
{
    public const string IdentityNumber = "identityNumber";
    public const string PassportNumber = "passportNumber";
    public const string BusinessRegistrationNumber = "businessRegistrationNumber";
}
