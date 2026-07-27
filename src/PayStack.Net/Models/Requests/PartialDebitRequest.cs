namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /transaction/partial_debit</c> — charges as much of <see cref="Amount"/> as the customer's balance allows.</summary>
public sealed class PartialDebitRequest
{
    /// <summary>The reusable authorization code to charge. Required.</summary>
    public required string AuthorizationCode { get; set; }

    /// <summary>ISO 4217 currency code. Only NGN and GHS are supported. Required.</summary>
    public required string Currency { get; set; }

    /// <summary>Amount to attempt to charge, in the currency's subunit. Required.</summary>
    public required string Amount { get; set; }

    /// <summary>Customer's email address. Required.</summary>
    public required string Email { get; set; }

    /// <summary>Unique transaction reference. Auto-generated when omitted.</summary>
    public string? Reference { get; set; }

    /// <summary>Minimum amount (in subunits) to accept if the full <see cref="Amount"/> can't be debited.</summary>
    public string? AtLeast { get; set; }
}
