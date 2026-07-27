using System.Text.Json;

namespace PayStack.Net.Models.Responses;

/// <summary>A country supported by Paystack, as returned by the "list countries" endpoint.</summary>
public sealed class CountryData
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? IsoCode { get; set; }

    public string? DefaultCurrencyCode { get; set; }

    /// <summary>Integration default configuration for this country. Shape varies by country/integration, so exposed as raw JSON.</summary>
    public JsonElement? IntegrationDefaults { get; set; }

    /// <summary>Supported currencies, integration features/types, and payment methods for this country. Shape varies, so exposed as raw JSON.</summary>
    public JsonElement? Relationships { get; set; }
}
