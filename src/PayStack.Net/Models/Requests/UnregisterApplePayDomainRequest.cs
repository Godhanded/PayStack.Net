namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>DELETE /apple-pay/domain</c>.</summary>
public sealed class UnregisterApplePayDomainRequest
{
    /// <summary>The domain name to unregister from Apple Pay. Required.</summary>
    public required string DomainName { get; set; }
}
