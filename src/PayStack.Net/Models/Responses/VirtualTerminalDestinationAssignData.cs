namespace PayStack.Net.Models.Responses;

/// <summary>A destination newly assigned to a virtual terminal, returned by the assign-destination endpoint.</summary>
public sealed class VirtualTerminalDestinationAssignData
{
    public long? Integration { get; set; }

    public string? Target { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public long Id { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}
