namespace PayStack.Net.Models.Responses;

/// <summary>A Paystack terminal (POS device), returned by list/fetch on the Terminal API.</summary>
public sealed class TerminalData
{
    public long Id { get; set; }

    public string? SerialNumber { get; set; }

    public string? DeviceMake { get; set; }

    /// <summary>The terminal's public identifier, used as the <c>terminal_id</c> route parameter elsewhere in this API.</summary>
    public string TerminalId { get; set; } = string.Empty;

    public long? Integration { get; set; }

    public string? Domain { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Status { get; set; }
}
