namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known values for <see cref="Responses.TransferData.Status"/>. See <see cref="PayStackChannel"/>
/// for the rationale on using string constants instead of an enum.
/// </summary>
public static class PayStackTransferStatus
{
    /// <summary>Transfer is queued and will complete without further action (OTP disabled).</summary>
    public const string Pending = "pending";

    /// <summary>Transfer is waiting on an OTP to be submitted via Finalize Transfer.</summary>
    public const string Otp = "otp";

    public const string Processing = "processing";
    public const string Success = "success";
    public const string Failed = "failed";
    public const string Reversed = "reversed";
}
