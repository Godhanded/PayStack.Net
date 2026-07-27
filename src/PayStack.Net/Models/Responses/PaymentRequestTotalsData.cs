namespace PayStack.Net.Models.Responses;

/// <summary>Aggregate payment request totals, broken down by currency, returned by <c>GET /paymentrequest/totals</c>.</summary>
public sealed class PaymentRequestTotalsData
{
    public List<PaymentRequestCurrencyTotal> Pending { get; set; } = [];

    public List<PaymentRequestCurrencyTotal> Successful { get; set; } = [];

    public List<PaymentRequestCurrencyTotal> Total { get; set; } = [];
}

/// <summary>A currency/amount pair within <see cref="PaymentRequestTotalsData"/>.</summary>
public sealed class PaymentRequestCurrencyTotal
{
    public string Currency { get; set; } = string.Empty;

    /// <summary>Amount, in the currency's subunit.</summary>
    public long Amount { get; set; }
}
