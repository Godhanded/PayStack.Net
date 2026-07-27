namespace PayStack.Net.Models.Responses;

/// <summary>Response payload from <c>POST /transaction/initialize</c>.</summary>
public sealed class InitializeTransactionData
{
    /// <summary>URL to redirect the customer to in order to complete payment.</summary>
    public string AuthorizationUrl { get; set; } = string.Empty;

    /// <summary>Code that can be used to render Paystack's inline/popup checkout for this transaction.</summary>
    public string AccessCode { get; set; } = string.Empty;

    /// <summary>The transaction reference (generated automatically if not supplied on the request).</summary>
    public string Reference { get; set; } = string.Empty;
}
