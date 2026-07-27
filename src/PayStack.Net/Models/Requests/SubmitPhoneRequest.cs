namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /charge/submit_phone</c> — continues a pending charge that requested a phone number.</summary>
public sealed class SubmitPhoneRequest
{
    /// <summary>The customer's phone number. Required.</summary>
    public required string Phone { get; set; }

    /// <summary>The reference of the pending charge. Required.</summary>
    public required string Reference { get; set; }
}
