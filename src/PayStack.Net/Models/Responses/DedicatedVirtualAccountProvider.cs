namespace PayStack.Net.Models.Responses;

/// <summary>A bank provider available for creating dedicated virtual accounts.</summary>
public sealed class DedicatedVirtualAccountProvider
{
    public string? ProviderSlug { get; set; }

    public long BankId { get; set; }

    public string? BankName { get; set; }

    public long Id { get; set; }
}
