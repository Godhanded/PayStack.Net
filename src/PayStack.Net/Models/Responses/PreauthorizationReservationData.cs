using System.Text.Json;
using PayStack.Net.Models.Common;

namespace PayStack.Net.Models.Responses;

/// <summary>
/// A reserved preauthorization, returned by <c>POST /preauthorization/reserve_authorization</c> and
/// <c>GET /preauthorization/verify/:reference</c>. <see cref="CapturedAt"/> is only populated once
/// the reservation has been captured (verify can observe this transition; reserve cannot).
/// </summary>
public sealed class PreauthorizationReservationData
{
    public long Id { get; set; }

    public string? Domain { get; set; }

    /// <summary>See <see cref="Common.PayStackPreauthorizationStatus"/> for known values.</summary>
    public string Status { get; set; } = string.Empty;

    public string Reference { get; set; } = string.Empty;

    /// <summary>Reserved amount, in the currency's subunit.</summary>
    public long Amount { get; set; }

    /// <summary>
    /// Unlike most Paystack objects, this is a structured object (not a string) — see
    /// <see cref="PreauthorizationGatewayResponse"/>.
    /// </summary>
    public PreauthorizationGatewayResponse? GatewayResponse { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? ReleasedAt { get; set; }

    public DateTimeOffset? ExpiryDate { get; set; }

    /// <summary>Only populated once the reservation has been captured; see <c>GET /preauthorization/verify/:reference</c>.</summary>
    public DateTimeOffset? CapturedAt { get; set; }

    public string Currency { get; set; } = string.Empty;

    public JsonElement? Metadata { get; set; }

    public long? Fees { get; set; }

    public AuthorizationData? Authorization { get; set; }

    public CustomerSummary? Customer { get; set; }

    public long? MerchantId { get; set; }

    public string? MerchantName { get; set; }

    /// <summary>See <see cref="Common.PayStackExpireAction"/> for known values.</summary>
    public string? ExpireAction { get; set; }

    public string? SplitCode { get; set; }

    public JsonElement? Split { get; set; }
}

/// <summary>The structured gateway response embedded in a <see cref="PreauthorizationReservationData"/>.</summary>
public sealed class PreauthorizationGatewayResponse
{
    public string? AuthorizeResponse { get; set; }

    public string? Rrn { get; set; }
}
