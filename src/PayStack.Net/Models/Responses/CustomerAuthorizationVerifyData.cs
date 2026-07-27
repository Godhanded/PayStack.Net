namespace PayStack.Net.Models.Responses;

/// <summary>Response payload from <c>GET /customer/authorization/verify/:reference</c>.</summary>
public sealed class CustomerAuthorizationVerifyData
{
    public string AuthorizationCode { get; set; } = string.Empty;

    public string? Channel { get; set; }

    public string? Bank { get; set; }

    public bool Active { get; set; }

    public CustomerAuthorizationVerifyCustomer? Customer { get; set; }
}

/// <summary>The abbreviated customer object embedded in a <see cref="CustomerAuthorizationVerifyData"/>.</summary>
public sealed class CustomerAuthorizationVerifyCustomer
{
    public string Code { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}
