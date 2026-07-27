namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /storefront</c>.</summary>
public sealed class CreateStorefrontRequest
{
    /// <summary>Storefront name. Required.</summary>
    public required string Name { get; set; }

    /// <summary>Unique URL slug for the storefront. Auto-generated from <see cref="Name"/> when omitted.</summary>
    public string? Slug { get; set; }

    public string? Description { get; set; }
}
