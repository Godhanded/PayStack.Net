namespace PayStack.Net.Models.Requests;

/// <summary>A WhatsApp destination to attach to a virtual terminal, used when creating or assigning destinations.</summary>
public sealed class VirtualTerminalDestinationRequest
{
    /// <summary>WhatsApp phone number to send payment notifications to. Required.</summary>
    public required string Target { get; set; }

    /// <summary>Friendly name for this destination. Required.</summary>
    public required string Name { get; set; }
}
