namespace PayStack.Net.Models.Common;

/// <summary>Well-known values for <see cref="Responses.OrderData.Status"/>. See <see cref="PayStackChannel"/> for the rationale on using string constants instead of an enum.</summary>
public static class PayStackOrderStatus
{
    public const string Pending = "pending";
    public const string Success = "success";
}
