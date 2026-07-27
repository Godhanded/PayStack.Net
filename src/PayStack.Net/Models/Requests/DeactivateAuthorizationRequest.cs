namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /customer/authorization/deactivate</c>.</summary>
public sealed class DeactivateAuthorizationRequest
{
    /// <summary>The authorization code to deactivate. Required.</summary>
    public required string AuthorizationCode { get; set; }
}
