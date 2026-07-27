namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /terminal/:terminal_id/event</c>.</summary>
public sealed class SendTerminalEventRequest
{
    /// <summary>Event type: "invoice" or "transaction". Required. See <see cref="Common.PayStackTerminalEventType"/>.</summary>
    public required string Type { get; set; }

    /// <summary>
    /// Action to perform. Required. For <see cref="Type"/> "invoice": "process" or "view".
    /// For <see cref="Type"/> "transaction": "process" or "print".
    /// </summary>
    public required string Action { get; set; }

    /// <summary>
    /// The invoice/transaction payload to act on. Required. Populate <see cref="TerminalEventPayload.Id"/>
    /// always; populate <see cref="TerminalEventPayload.Reference"/> too when <see cref="Type"/> is "invoice".
    /// </summary>
    public required TerminalEventPayload Data { get; set; }
}

/// <summary>The invoice/transaction identifier carried in a terminal event's <c>data</c> field.</summary>
public sealed class TerminalEventPayload
{
    /// <summary>The invoice or transaction id. Required.</summary>
    public required string Id { get; set; }

    /// <summary>The invoice reference. Only used when the event type is "invoice".</summary>
    public string? Reference { get; set; }
}
