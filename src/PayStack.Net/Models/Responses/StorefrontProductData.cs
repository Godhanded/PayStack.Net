namespace PayStack.Net.Models.Responses;

/// <summary>An abbreviated product listing returned by <c>GET /storefront/:id/product</c>.</summary>
public sealed class StorefrontProductData
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    /// <summary>Price, in the currency's subunit.</summary>
    public long Price { get; set; }

    public string Currency { get; set; } = string.Empty;

    public bool Active { get; set; }
}
