namespace PayStack.Net.Models.Responses;

/// <summary>An order placed against a storefront, returned by <c>GET /storefront/:id/order</c>.</summary>
public sealed class StorefrontOrderData
{
    public long Id { get; set; }

    /// <summary>Public identifier for the order.</summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>Order total, in the currency's subunit.</summary>
    public long Amount { get; set; }

    public string Currency { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public StorefrontOrderCustomer? Customer { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }
}

/// <summary>The minimal customer identity embedded in a <see cref="StorefrontOrderData"/>.</summary>
public sealed class StorefrontOrderCustomer
{
    public string Email { get; set; } = string.Empty;
}
