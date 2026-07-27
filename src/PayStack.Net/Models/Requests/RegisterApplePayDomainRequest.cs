namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /apple-pay/domain</c>.</summary>
public sealed class RegisterApplePayDomainRequest
{
    /// <summary>The domain name to register with Apple Pay. Only one domain can be registered per call. Required.</summary>
    public required string DomainName { get; set; }
}
