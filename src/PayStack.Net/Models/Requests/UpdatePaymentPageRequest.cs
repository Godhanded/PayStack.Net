namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>PUT /page/{id_or_slug}</c>.</summary>
public sealed class UpdatePaymentPageRequest
{
    /// <summary>Name of the payment page. Required.</summary>
    public required string Name { get; set; }

    /// <summary>Paystack's docs describe this as required alongside <see cref="Name"/>, though it is nullable here defensively.</summary>
    public string? Description { get; set; }

    /// <summary>Amount to charge, in the currency's subunit.</summary>
    public long? Amount { get; set; }

    /// <summary>Set to <c>false</c> to deactivate the page.</summary>
    public bool? Active { get; set; }
}
