using System.Text.Json;
using PayStack.Net.Models.Common;

namespace PayStack.Net.Models.Responses;

/// <summary>Response payload from <c>POST /preauthorization/capture</c>.</summary>
public sealed class PreauthChargeData
{
    public long Id { get; set; }

    /// <summary>Amount captured, in the currency's subunit.</summary>
    public long Amount { get; set; }

    public string Currency { get; set; } = string.Empty;

    public DateTimeOffset? TransactionDate { get; set; }

    /// <summary>Capture status. See <see cref="PayStackTransactionStatus"/> for known values.</summary>
    public string Status { get; set; } = string.Empty;

    public string Reference { get; set; } = string.Empty;

    public string? Domain { get; set; }

    public JsonElement? Metadata { get; set; }

    public string? GatewayResponse { get; set; }

    public string? Message { get; set; }

    public string? Channel { get; set; }

    public string? IpAddress { get; set; }

    public TransactionLog? Log { get; set; }

    /// <summary>Total fee charged for this capture, in subunits.</summary>
    public long? Fees { get; set; }

    public AuthorizationData? Authorization { get; set; }

    public CustomerSummary? Customer { get; set; }

    public JsonElement? Plan { get; set; }
}
