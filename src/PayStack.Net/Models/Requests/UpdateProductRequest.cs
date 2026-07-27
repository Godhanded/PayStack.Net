namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>PUT /product/:id</c>.</summary>
public sealed class UpdateProductRequest
{
    /// <summary>Product name. Required.</summary>
    public required string Name { get; set; }

    /// <summary>Product description. Required.</summary>
    public required string Description { get; set; }

    /// <summary>Price, in the currency's subunit. Required.</summary>
    public required long Price { get; set; }

    /// <summary>ISO 4217 currency code. Required.</summary>
    public required string Currency { get; set; }

    /// <summary>Whether stock is unlimited. When <c>false</c>, set <see cref="Quantity"/>.</summary>
    public bool? Unlimited { get; set; }

    /// <summary>Quantity in stock. Used only when <see cref="Unlimited"/> is <c>false</c>.</summary>
    public int? Quantity { get; set; }
}
