namespace PayStack.Net.Models.Requests;

/// <summary>
/// Request body for <c>POST /preauthorization/reserve_authorization</c> — reserves an amount against
/// an existing reusable authorization, to be captured or released later.
/// </summary>
public sealed class ReservePreauthorizationRequest
{
    /// <summary>Customer's email address. Required.</summary>
    public required string Email { get; set; }

    /// <summary>Amount to reserve, in the currency's subunit. Required.</summary>
    public required string Amount { get; set; }

    /// <summary>The reusable authorization code to reserve against. Required.</summary>
    public required string AuthorizationCode { get; set; }

    /// <summary>ISO 4217 currency code. Only "ZAR" is supported. Required.</summary>
    public required string Currency { get; set; }

    /// <summary>Unique transaction reference. Auto-generated when omitted.</summary>
    public string? Reference { get; set; }
}
