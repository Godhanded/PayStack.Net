namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /charge/submit_otp</c> — continues a pending charge that requested an OTP.</summary>
public sealed class SubmitOtpRequest
{
    /// <summary>The one-time-pin sent to the customer. Required.</summary>
    public required string Otp { get; set; }

    /// <summary>The reference of the pending charge. Required.</summary>
    public required string Reference { get; set; }
}
