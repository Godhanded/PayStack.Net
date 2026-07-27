namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /transfer/finalize_transfer</c>.</summary>
public sealed class FinalizeTransferRequest
{
    /// <summary>The transfer code ("TRF_xxx") returned by Initiate Transfer. Required.</summary>
    public required string TransferCode { get; set; }

    /// <summary>The OTP sent to complete the transfer. Required.</summary>
    public required string Otp { get; set; }
}
