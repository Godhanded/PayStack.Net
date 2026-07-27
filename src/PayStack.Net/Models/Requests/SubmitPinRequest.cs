namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /charge/submit_pin</c> — continues a pending charge that requested a card PIN.</summary>
public sealed class SubmitPinRequest
{
    /// <summary>The 4-digit card PIN. Required.</summary>
    public required string Pin { get; set; }

    /// <summary>The reference of the pending charge. Required.</summary>
    public required string Reference { get; set; }
}
