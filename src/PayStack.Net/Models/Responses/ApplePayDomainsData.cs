namespace PayStack.Net.Models.Responses;

/// <summary>The set of domains currently registered for Apple Pay on your integration.</summary>
public sealed class ApplePayDomainsData
{
    public List<string>? DomainNames { get; set; }
}
