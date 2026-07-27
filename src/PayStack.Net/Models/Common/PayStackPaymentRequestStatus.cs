namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known values for <see cref="Responses.PaymentRequestData.Status"/>. Not an exhaustive list —
/// Paystack's docs only confirm "pending" and "success" explicitly. See <see cref="PayStackChannel"/>
/// for the rationale on using string constants instead of an enum.
/// </summary>
public static class PayStackPaymentRequestStatus
{
    public const string Pending = "pending";
    public const string Success = "success";
}
