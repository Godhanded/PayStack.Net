using System.Text.Json;

namespace PayStack.Net.Models.Responses;

/// <summary>
/// A Paystack payment page, returned by create/list/fetch/update/add-products.
/// Not every field is populated by every endpoint — e.g. <see cref="Products"/> is only
/// populated by the "fetch" and "add products" endpoints.
/// </summary>
public sealed class PaymentPageData
{
    /// <summary>Paystack's internal numeric payment page id.</summary>
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    /// <summary>Fixed amount for the page, in the currency's subunit, or <c>null</c> for a flexible-amount page.</summary>
    public long? Amount { get; set; }

    public string? Currency { get; set; }

    public string Slug { get; set; } = string.Empty;

    /// <summary>Page type, e.g. "payment", "subscription". See <see cref="Common.PayStackPaymentPageType"/>.</summary>
    public string? Type { get; set; }

    /// <summary>Plan id the page is tied to, when <see cref="Type"/> is "subscription".</summary>
    public long? Plan { get; set; }

    public string? SplitCode { get; set; }

    /// <summary>Custom form fields collected on the page. Shape is caller-defined, so exposed as raw JSON.</summary>
    public List<JsonElement>? CustomFields { get; set; }

    public string? RedirectUrl { get; set; }

    public string? SuccessMessage { get; set; }

    public string? NotificationEmail { get; set; }

    public bool CollectPhone { get; set; }

    /// <summary>Arbitrary metadata attached to the page, e.g. subaccount/logo/transaction-charge details.</summary>
    public JsonElement? Metadata { get; set; }

    public bool Active { get; set; }

    public bool Published { get; set; }

    public bool? Migrate { get; set; }

    public string? Domain { get; set; }

    public long Integration { get; set; }

    /// <summary>Products attached to the page. Only populated by "fetch" and "add products".</summary>
    public List<PaymentPageProduct>? Products { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}

/// <summary>A product attached to a payment page (populated when fetching a page or adding products to it).</summary>
public sealed class PaymentPageProduct
{
    public long ProductId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? ProductCode { get; set; }

    public long? Page { get; set; }

    /// <summary>Price of the product, in the currency's subunit.</summary>
    public long Price { get; set; }

    public string? Currency { get; set; }

    public int Quantity { get; set; }

    public string? Type { get; set; }

    /// <summary>Freeform feature description text; shape is not fixed, exposed as raw JSON.</summary>
    public JsonElement? Features { get; set; }

    public bool IsShippable { get; set; }

    public string? Domain { get; set; }

    public long? Integration { get; set; }

    public bool Active { get; set; }

    public bool InStock { get; set; }
}
