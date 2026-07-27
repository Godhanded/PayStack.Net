namespace PayStack.Net.Models.Responses;

/// <summary>The result of resolving a bank account number to its account name.</summary>
public sealed class ResolvedAccountData
{
    public string AccountNumber { get; set; } = string.Empty;

    public string AccountName { get; set; } = string.Empty;
}
