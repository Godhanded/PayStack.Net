namespace PayStack.Net.Models.Requests;

/// <summary>
/// Request body for <c>POST /subscription/disable</c>. This is Paystack's only way to cancel a
/// subscription — there is no DELETE endpoint.
/// </summary>
public sealed class DisableSubscriptionRequest
{
    /// <summary>The subscription code to disable. Required.</summary>
    public required string Code { get; set; }

    /// <summary>The subscription's email token (returned as <c>email_token</c> on create/fetch). Required.</summary>
    public required string Token { get; set; }
}
