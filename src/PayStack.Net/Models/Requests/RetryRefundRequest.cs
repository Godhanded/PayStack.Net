namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /refund/retry_with_customer_details/:id</c>.</summary>
public sealed class RetryRefundRequest
{
    /// <summary>Bank account details to send the refund to. Required.</summary>
    public required RefundAccountDetails RefundAccountDetails { get; set; }
}

/// <summary>Bank account details supplied when retrying a failed refund.</summary>
public sealed class RefundAccountDetails
{
    /// <summary>ISO 4217 currency code. Required.</summary>
    public required string Currency { get; set; }

    /// <summary>Destination bank account number. Required.</summary>
    public required string AccountNumber { get; set; }

    /// <summary>Destination bank id, as returned by the List Banks endpoint. Required.</summary>
    public required string BankId { get; set; }
}
