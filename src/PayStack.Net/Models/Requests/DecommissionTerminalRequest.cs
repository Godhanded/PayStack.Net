namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /terminal/decommission_device</c>.</summary>
public sealed class DecommissionTerminalRequest
{
    /// <summary>Serial number of the terminal device to decommission. Required.</summary>
    public required string SerialNumber { get; set; }
}
