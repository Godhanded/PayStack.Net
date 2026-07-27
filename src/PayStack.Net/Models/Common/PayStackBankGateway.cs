namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known gateway values accepted by the "list banks" <c>gateway</c> filter.
/// Modeled as string constants rather than a C# <c>enum</c> so the SDK stays forward-compatible
/// when Paystack adds a new gateway.
/// </summary>
public static class PayStackBankGateway
{
    public const string Emandate = "emandate";
    public const string DigitalBankMandate = "digitalbankmandate";
}
