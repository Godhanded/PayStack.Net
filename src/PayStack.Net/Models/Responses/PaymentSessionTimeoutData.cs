namespace PayStack.Net.Models.Responses;

/// <summary>The configured payment session timeout, returned by fetch/update on the Integration API.</summary>
public sealed class PaymentSessionTimeoutData
{
    /// <summary>Seconds a checkout session stays open before it times out. <c>0</c> means timeouts are disabled.</summary>
    public int PaymentSessionTimeout { get; set; }
}
