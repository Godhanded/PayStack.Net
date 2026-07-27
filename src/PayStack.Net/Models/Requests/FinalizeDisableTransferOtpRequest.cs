namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /transfer/disable_otp_finalize</c>.</summary>
public sealed class FinalizeDisableTransferOtpRequest
{
    /// <summary>The OTP sent to confirm disabling the Transfers OTP requirement. Required.</summary>
    public required string Otp { get; set; }
}
