namespace PayStack.Net.Models.Responses;

/// <summary>
/// Response payload from <c>POST /customer/authorization/initialize</c> and
/// <c>POST /customer/:id/initialize-direct-debit</c> — both return this identical shape.
/// </summary>
public sealed class CustomerAuthorizationInitializeData
{
    /// <summary>URL to redirect the customer to in order to complete authorization setup.</summary>
    public string RedirectUrl { get; set; } = string.Empty;

    public string AccessCode { get; set; } = string.Empty;

    public string Reference { get; set; } = string.Empty;
}
