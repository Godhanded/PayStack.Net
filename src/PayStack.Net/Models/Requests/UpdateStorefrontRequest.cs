namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>PUT /storefront/:id</c>.</summary>
public sealed class UpdateStorefrontRequest
{
    public string? Name { get; set; }

    public string? Description { get; set; }
}
