namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>PUT /transferrecipient/:id_or_code</c>.</summary>
public sealed class UpdateTransferRecipientRequest
{
    /// <summary>Recipient's name. Required.</summary>
    public required string Name { get; set; }

    public string? Email { get; set; }
}
