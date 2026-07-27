namespace PayStack.Net.Models.Responses;

/// <summary>Response payload from <c>POST /preauthorization/initialize</c>.</summary>
public sealed class InitializePreauthorizationData
{
    /// <summary>URL to redirect the customer to in order to complete the preauthorization.</summary>
    public string AuthorizationUrl { get; set; } = string.Empty;

    public string AccessCode { get; set; } = string.Empty;

    public string Reference { get; set; } = string.Empty;
}
