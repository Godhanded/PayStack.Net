using System.Text.Json;

namespace PayStack.Net.Models.Responses;

/// <summary>
/// A Paystack virtual terminal, returned by create/list/fetch/update on the Virtual Terminal API.
/// Not every field is populated by every endpoint — e.g. <see cref="Destinations"/> and
/// <see cref="ConnectAccountId"/> are only populated by fetch, and <see cref="Active"/>/
/// <see cref="CreatedAt"/> only by list/fetch.
/// </summary>
public sealed class VirtualTerminalData
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public long? Integration { get; set; }

    public string? Domain { get; set; }

    /// <summary>Public code identifying this virtual terminal, e.g. "VT_xxx".</summary>
    public string Code { get; set; } = string.Empty;

    public List<string>? PaymentMethods { get; set; }

    public bool? Active { get; set; }

    /// <summary>Arbitrary metadata previously attached to the virtual terminal.</summary>
    public JsonElement? Metadata { get; set; }

    public List<VirtualTerminalDestinationData>? Destinations { get; set; }

    public string? Currency { get; set; }

    /// <summary>Only populated by <c>FetchAsync</c>.</summary>
    public string? ConnectAccountId { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }
}

/// <summary>A WhatsApp destination attached to a virtual terminal.</summary>
public sealed class VirtualTerminalDestinationData
{
    public string? Target { get; set; }

    public string? Type { get; set; }

    public string? Name { get; set; }

    /// <summary>Only populated when fetching a single virtual terminal.</summary>
    public DateTimeOffset? CreatedAt { get; set; }
}
