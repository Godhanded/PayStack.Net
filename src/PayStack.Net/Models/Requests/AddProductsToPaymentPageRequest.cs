namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /page/{id}/product</c>.</summary>
public sealed class AddProductsToPaymentPageRequest
{
    /// <summary>Ids of the products to attach to the page. Required.</summary>
    public required List<long> Product { get; set; }
}
