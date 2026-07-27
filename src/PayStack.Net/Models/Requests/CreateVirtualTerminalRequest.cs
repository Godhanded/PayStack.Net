namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /virtual_terminal</c>.</summary>
public sealed class CreateVirtualTerminalRequest
{
    /// <summary>Name of the virtual terminal. Required.</summary>
    public required string Name { get; set; }

    /// <summary>WhatsApp destinations to notify of payments made through this virtual terminal. Required.</summary>
    public required List<VirtualTerminalDestinationRequest> Destinations { get; set; }

    /// <summary>Arbitrary metadata, as a stringified JSON object.</summary>
    public string? Metadata { get; set; }

    /// <summary>ISO 4217 currency code. Defaults to your integration's default currency.</summary>
    public string? Currency { get; set; }

    /// <summary>Additional custom fields to collect/display at payment time.</summary>
    public List<VirtualTerminalCustomField>? CustomFields { get; set; }
}

/// <summary>A custom field displayed on a virtual terminal's payment page.</summary>
public sealed class VirtualTerminalCustomField
{
    /// <summary>Label shown to the payer. Required.</summary>
    public required string DisplayName { get; set; }

    /// <summary>Machine-readable field name. Required.</summary>
    public required string VariableName { get; set; }
}
