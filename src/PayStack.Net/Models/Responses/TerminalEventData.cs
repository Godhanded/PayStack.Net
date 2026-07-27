namespace PayStack.Net.Models.Responses;

/// <summary>The id of a queued terminal event, returned by <c>POST /terminal/:terminal_id/event</c>.</summary>
public sealed class TerminalEventData
{
    /// <summary>Id of the queued event, used to poll <c>FetchEventStatusAsync</c>.</summary>
    public string Id { get; set; } = string.Empty;
}
