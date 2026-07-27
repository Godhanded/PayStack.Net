namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>PUT /directdebit/activation-charge</c> — retries the activation charge for customers with pending mandate authorizations.</summary>
public sealed class TriggerActivationChargeRequest
{
    /// <summary>Ids of the customers whose pending mandates should be retried. Required.</summary>
    public required List<long> CustomerIds { get; set; }
}
