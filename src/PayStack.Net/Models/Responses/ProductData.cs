using System.Text.Json;

namespace PayStack.Net.Models.Responses;

/// <summary>
/// A Paystack product, returned by create/list/fetch/update product endpoints.
/// </summary>
public sealed class ProductData
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    /// <summary>Public identifier for the product, e.g. "PROD_xxx".</summary>
    public string ProductCode { get; set; } = string.Empty;

    public string? Slug { get; set; }

    public string Currency { get; set; } = string.Empty;

    /// <summary>Price, in the currency's subunit.</summary>
    public long Price { get; set; }

    public int Quantity { get; set; }

    public int? QuantitySold { get; set; }

    public bool Active { get; set; }

    public string? Domain { get; set; }

    /// <summary>Product type, e.g. "good".</summary>
    public string? Type { get; set; }

    public bool InStock { get; set; }

    public bool Unlimited { get; set; }

    /// <summary>Arbitrary metadata (e.g. a display <c>background_color</c>), or <c>null</c>.</summary>
    public JsonElement? Metadata { get; set; }

    public List<ProductFile>? Files { get; set; }

    public string? SuccessMessage { get; set; }

    public string? RedirectUrl { get; set; }

    /// <summary>Transaction split code applied to sales of this product, if any.</summary>
    public string? SplitCode { get; set; }

    public string? NotificationEmails { get; set; }

    public int MinimumOrderable { get; set; }

    public int? MaximumOrderable { get; set; }

    /// <summary>
    /// Whether a low-stock alert is enabled. Paystack's docs are inconsistent about whether this is a
    /// boolean or an integer threshold, so it's exposed as a raw <see cref="JsonElement"/> — check
    /// <see cref="JsonElement.ValueKind"/> before reading it.
    /// </summary>
    public JsonElement? LowStockAlert { get; set; }

    public bool IsShippable { get; set; }

    public ProductShippingFields? ShippingFields { get; set; }

    public int Integration { get; set; }

    /// <summary>Digital assets attached to the product. Only populated on fetch-by-id.</summary>
    public List<JsonElement>? DigitalAssets { get; set; }

    /// <summary>Path to the primary product file. Only populated on fetch-by-id.</summary>
    public string? FilePath { get; set; }

    /// <summary>Feature list. Only populated on fetch-by-id.</summary>
    public List<JsonElement>? Features { get; set; }

    /// <summary>Only populated on fetch-by-id.</summary>
    public int? StockThreshold { get; set; }

    /// <summary>Only populated on fetch-by-id.</summary>
    public JsonElement? ExpiresIn { get; set; }

    public List<JsonElement>? VariantOptions { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}

/// <summary>A file attached to a product listing.</summary>
public sealed class ProductFile
{
    public string? Key { get; set; }

    public string? Type { get; set; }

    public string? Path { get; set; }

    public string? OriginalFilename { get; set; }
}

/// <summary>Shipping configuration for a physical product.</summary>
public sealed class ProductShippingFields
{
    public string? DeliveryNote { get; set; }

    public List<ProductShippingFee>? ShippingFees { get; set; }
}

/// <summary>A per-region shipping fee for a product.</summary>
public sealed class ProductShippingFee
{
    public string? Region { get; set; }

    /// <summary>Shipping fee, in the currency's subunit.</summary>
    public long? Fee { get; set; }

    public string? Currency { get; set; }
}
