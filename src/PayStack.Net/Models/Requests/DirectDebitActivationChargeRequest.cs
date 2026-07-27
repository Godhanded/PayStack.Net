namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>PUT /customer/:id/directdebit-activation-charge</c>.</summary>
public sealed class DirectDebitActivationChargeRequest
{
    /// <summary>The pending mandate authorization's id. Required.</summary>
    public required long AuthorizationId { get; set; }
}
