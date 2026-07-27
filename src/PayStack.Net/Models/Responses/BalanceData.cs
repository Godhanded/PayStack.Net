namespace PayStack.Net.Models.Responses;

/// <summary>A per-currency balance on your Paystack integration, returned by <c>GET /balance</c>.</summary>
public sealed class BalanceData
{
    public string Currency { get; set; } = string.Empty;

    /// <summary>Available balance, in the currency's subunit.</summary>
    public long Balance { get; set; }
}
