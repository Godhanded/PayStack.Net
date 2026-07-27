namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /order</c>.</summary>
public sealed class CreateOrderRequest
{
    /// <summary>Customer code or id the order is for. Required.</summary>
    public required string Customer { get; set; }

    /// <summary>Products and quantities that make up the order. Required.</summary>
    public required List<CreateOrderLineItem> LineItems { get; set; }
}

/// <summary>A single line item within a <see cref="CreateOrderRequest"/>.</summary>
public sealed class CreateOrderLineItem
{
    /// <summary>Paystack product id. Required.</summary>
    public required long Product { get; set; }

    /// <summary>Number of units of the product. Required.</summary>
    public required int Quantity { get; set; }
}
