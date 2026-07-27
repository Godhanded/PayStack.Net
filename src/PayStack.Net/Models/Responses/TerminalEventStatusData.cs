namespace PayStack.Net.Models.Responses;

/// <summary>Delivery status of a terminal event, returned by <c>GET /terminal/:terminal_id/event/:event_id</c>.</summary>
public sealed class TerminalEventStatusData
{
    /// <summary>Whether the event was delivered to the terminal.</summary>
    public bool Delivered { get; set; }
}
