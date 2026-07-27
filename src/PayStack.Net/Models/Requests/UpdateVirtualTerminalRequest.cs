namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>PUT /virtual_terminal/:code</c>.</summary>
public sealed class UpdateVirtualTerminalRequest
{
    /// <summary>New name for the virtual terminal. Required.</summary>
    public required string Name { get; set; }
}
