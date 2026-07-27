namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>PUT /customer/:code</c>.</summary>
public sealed class UpdateCustomerRequest
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Phone { get; set; }

    /// <summary>Arbitrary JSON-serializable data to attach to the customer.</summary>
    public object? Metadata { get; set; }
}
