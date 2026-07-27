namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>PUT /integration/payment_session_timeout</c>.</summary>
public sealed class UpdatePaymentSessionTimeoutRequest
{
    /// <summary>Seconds a checkout session should stay open before timing out. Set to <c>0</c> to cancel the timeout. Required.</summary>
    public required int Timeout { get; set; }
}
