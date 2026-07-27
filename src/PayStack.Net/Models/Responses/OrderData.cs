namespace PayStack.Net.Models.Responses;

/// <summary>
/// A Paystack "pay for me" order, returned by create/list/fetch/fetch-product-orders.
/// Not every field is populated by every endpoint — list endpoints return a lighter version
/// (e.g. <see cref="Customer"/> may omit <see cref="OrderCustomer.Id"/>).
/// </summary>
public sealed class OrderData
{
    /// <summary>Paystack's internal numeric order id.</summary>
    public long Id { get; set; }

    /// <summary>Public identifier for the order, e.g. "ORD_xxx".</summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>Total order amount, in the currency's subunit.</summary>
    public long Amount { get; set; }

    public string Currency { get; set; } = string.Empty;

    /// <summary>Order status, e.g. "pending", "success". See <see cref="Common.PayStackOrderStatus"/>.</summary>
    public string Status { get; set; } = string.Empty;

    public OrderCustomer? Customer { get; set; }

    /// <summary>Only populated on the "fetch" endpoint.</summary>
    public List<OrderLineItem>? LineItems { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }
}

/// <summary>Abbreviated customer reference embedded in an <see cref="OrderData"/>.</summary>
public sealed class OrderCustomer
{
    /// <summary>Only populated on the "fetch" endpoint; list endpoints omit it.</summary>
    public long? Id { get; set; }

    public string Email { get; set; } = string.Empty;
}

/// <summary>A single line item within an order.</summary>
public sealed class OrderLineItem
{
    public OrderLineItemProduct Product { get; set; } = new();

    public int Quantity { get; set; }

    /// <summary>Line amount, in the currency's subunit.</summary>
    public long Amount { get; set; }
}

/// <summary>The product referenced by an <see cref="OrderLineItem"/>.</summary>
public sealed class OrderLineItemProduct
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;
}

/// <summary>Result of validating a "pay for me" order by its code.</summary>
public sealed class OrderValidationData
{
    public long Id { get; set; }

    public string Code { get; set; } = string.Empty;

    /// <summary>Order amount, in the currency's subunit.</summary>
    public long Amount { get; set; }

    public string Currency { get; set; } = string.Empty;

    /// <summary>Order status, e.g. "pending", "success". See <see cref="Common.PayStackOrderStatus"/>.</summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>Whether the order is valid/payable.</summary>
    public bool Valid { get; set; }
}
