namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /virtual_terminal/:code/destination/unassign</c>.</summary>
public sealed class UnassignVirtualTerminalDestinationRequest
{
    /// <summary>WhatsApp phone numbers (destination targets) to remove from the virtual terminal. Required.</summary>
    public required List<string> Targets { get; set; }
}
