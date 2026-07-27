namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /charge/submit_birthday</c> — continues a pending charge that requested the customer's date of birth.</summary>
public sealed class SubmitBirthdayRequest
{
    /// <summary>The customer's date of birth, formatted "YYYY-MM-DD". Required.</summary>
    public required string Birthday { get; set; }

    /// <summary>The reference of the pending charge. Required.</summary>
    public required string Reference { get; set; }
}
