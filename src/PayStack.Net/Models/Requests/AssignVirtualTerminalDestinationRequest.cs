namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /virtual_terminal/:code/destination/assign</c>.</summary>
public sealed class AssignVirtualTerminalDestinationRequest
{
    /// <summary>WhatsApp destinations to assign to the virtual terminal. Required.</summary>
    public required List<VirtualTerminalDestinationRequest> Destinations { get; set; }
}
