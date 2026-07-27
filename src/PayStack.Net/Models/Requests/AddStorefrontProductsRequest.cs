namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /storefront/:id/product</c>.</summary>
public sealed class AddStorefrontProductsRequest
{
    /// <summary>Paystack product ids to add to the storefront. Required.</summary>
    public required List<long> Products { get; set; }
}
