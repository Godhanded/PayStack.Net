namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known payment channel values accepted by <c>channels</c> parameters across the API.
/// Modeled as string constants rather than a C# <c>enum</c> so the SDK stays forward-compatible
/// when Paystack adds a new channel — you can still pass any raw string, these are just the
/// documented ones with IntelliSense support.
/// </summary>
public static class PayStackChannel
{
    public const string Card = "card";
    public const string Bank = "bank";
    public const string ApplePay = "apple_pay";
    public const string Ussd = "ussd";
    public const string Qr = "qr";
    public const string MobileMoney = "mobile_money";
    public const string BankTransfer = "bank_transfer";
    public const string Eft = "eft";
    public const string CapitecPay = "capitec_pay";
    public const string Payattitude = "payattitude";
}
