namespace PayStack.Net.Models.Responses;

/// <summary>
/// A Paystack storefront, returned by create/list/fetch/update storefront endpoints.
/// </summary>
public sealed class StorefrontData
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public string? Description { get; set; }

    /// <summary>Storefront status, e.g. "active", "inactive". See <see cref="Common.PayStackStorefrontStatus"/> for known values.</summary>
    public string Status { get; set; } = string.Empty;

    public string Currency { get; set; } = string.Empty;

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}
