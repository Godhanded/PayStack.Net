namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /refund</c>.</summary>
public sealed class CreateRefundRequest
{
    /// <summary>The transaction reference or numeric id to refund. Required.</summary>
    public required string Transaction { get; set; }

    /// <summary>Amount to refund, in the currency's subunit. Defaults to the full transaction amount when omitted.</summary>
    public long? Amount { get; set; }

    /// <summary>ISO 4217 currency code. Defaults to the transaction's currency.</summary>
    public string? Currency { get; set; }

    /// <summary>Note visible to the customer explaining the refund.</summary>
    public string? CustomerNote { get; set; }

    /// <summary>Internal note about the refund, not shown to the customer.</summary>
    public string? MerchantNote { get; set; }
}
