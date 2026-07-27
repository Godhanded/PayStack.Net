namespace PayStack.Net.Models.Requests;

/// <summary>
/// Request body for <c>POST /customer</c>. <see cref="FirstName"/>, <see cref="LastName"/>, and
/// <see cref="Phone"/> become required by Paystack when the customer will get a Dedicated Virtual
/// Account and your business category is Betting, Financial Services, or General Services.
/// </summary>
public sealed class CreateCustomerRequest
{
    /// <summary>Customer's email address. Required.</summary>
    public required string Email { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Phone { get; set; }

    /// <summary>Arbitrary JSON-serializable data to attach to the customer.</summary>
    public object? Metadata { get; set; }
}
