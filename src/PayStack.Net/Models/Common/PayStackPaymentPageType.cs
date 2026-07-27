namespace PayStack.Net.Models.Common;

/// <summary>Well-known values for a payment page's <c>type</c> field. See <see cref="PayStackChannel"/> for the rationale on using string constants instead of an enum.</summary>
public static class PayStackPaymentPageType
{
    public const string Payment = "payment";
    public const string Subscription = "subscription";
    public const string Product = "product";
    public const string Plan = "plan";
}
