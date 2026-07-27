namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /subscription/enable</c>.</summary>
public sealed class EnableSubscriptionRequest
{
    /// <summary>The subscription code to enable. Required.</summary>
    public required string Code { get; set; }

    /// <summary>The subscription's email token (returned as <c>email_token</c> on create/fetch). Required.</summary>
    public required string Token { get; set; }
}
