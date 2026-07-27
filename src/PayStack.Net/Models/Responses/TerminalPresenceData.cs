namespace PayStack.Net.Models.Responses;

/// <summary>Live connectivity status of a terminal, returned by <c>GET /terminal/:terminal_id/presence</c>.</summary>
public sealed class TerminalPresenceData
{
    /// <summary>Whether the terminal currently has a network connection.</summary>
    public bool Online { get; set; }

    /// <summary>Whether the terminal is currently free to receive a new event.</summary>
    public bool Available { get; set; }
}
