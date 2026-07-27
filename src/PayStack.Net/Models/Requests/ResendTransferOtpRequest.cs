namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /transfer/resend_otp</c>.</summary>
public sealed class ResendTransferOtpRequest
{
    /// <summary>The transfer code ("TRF_xxx") to resend the OTP for. Required.</summary>
    public required string TransferCode { get; set; }

    /// <summary>Reason for resending: "resend_otp" or "transfer". Required.</summary>
    public required string Reason { get; set; }
}
