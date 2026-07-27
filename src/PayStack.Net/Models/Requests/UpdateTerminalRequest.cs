namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>PUT /terminal/:terminal_id</c>.</summary>
public sealed class UpdateTerminalRequest
{
    public string? Name { get; set; }

    public string? Address { get; set; }
}
