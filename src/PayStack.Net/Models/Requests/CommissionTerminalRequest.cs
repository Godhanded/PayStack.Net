namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /terminal/commission_device</c>.</summary>
public sealed class CommissionTerminalRequest
{
    /// <summary>Serial number of the terminal device to commission. Required.</summary>
    public required string SerialNumber { get; set; }
}
