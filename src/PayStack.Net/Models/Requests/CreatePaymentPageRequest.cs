using System.Text.Json;

namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /page</c>.</summary>
public sealed class CreatePaymentPageRequest
{
    /// <summary>Name of the payment page. Required.</summary>
    public required string Name { get; set; }

    public string? Description { get; set; }

    /// <summary>Amount to charge, in the currency's subunit. Required when <see cref="FixedAmount"/> is <c>true</c>.</summary>
    public long? Amount { get; set; }

    /// <summary>ISO 4217 currency code. Defaults to your integration's default currency.</summary>
    public string? Currency { get; set; }

    /// <summary>Custom URL slug for the page. Auto-generated from <see cref="Name"/> when omitted.</summary>
    public string? Slug { get; set; }

    /// <summary>Page type: "payment", "subscription", "product", or "plan". See <see cref="Common.PayStackPaymentPageType"/>. Defaults to "payment".</summary>
    public string? Type { get; set; }

    /// <summary>Plan id or code to subscribe payers to, when <see cref="Type"/> is "subscription".</summary>
    public string? Plan { get; set; }

    /// <summary>When <c>true</c>, payers must pay exactly <see cref="Amount"/> rather than choosing their own amount.</summary>
    public bool? FixedAmount { get; set; }

    /// <summary>Split code of a pre-created transaction split to apply.</summary>
    public string? SplitCode { get; set; }

    /// <summary>Arbitrary metadata, which can include subaccount, logo image, and transaction charge details.</summary>
    public object? Metadata { get; set; }

    public string? RedirectUrl { get; set; }

    public string? SuccessMessage { get; set; }

    public string? NotificationEmail { get; set; }

    public bool? CollectPhone { get; set; }

    /// <summary>Custom form fields to collect. Shape is caller-defined; each element should be a JSON object.</summary>
    public List<JsonElement>? CustomFields { get; set; }
}
