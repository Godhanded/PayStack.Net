using Refit;

namespace PayStack.Net.Models.Requests;

/// <summary>
/// Query parameters for <c>GET /paymentrequest</c>. Properties are explicitly aliased to their
/// literal camelCase query key, since Refit flattens complex <c>[Query]</c> objects using the
/// property name as-is (it does not apply the shared JSON naming policy).
/// </summary>
public sealed class ListPaymentRequestsQuery
{
    /// <summary>Records per page. Defaults to 50.</summary>
    [AliasAs("perPage")]
    public int? PerPage { get; set; }

    /// <summary>Page number (offset pagination). Defaults to 1.</summary>
    [AliasAs("page")]
    public int? Page { get; set; }

    /// <summary>Filter by customer id.</summary>
    [AliasAs("customer")]
    public string? Customer { get; set; }

    /// <summary>Filter by status. See <see cref="Common.PayStackPaymentRequestStatus"/>.</summary>
    [AliasAs("status")]
    public string? Status { get; set; }

    /// <summary>Filter by ISO 4217 currency code.</summary>
    [AliasAs("currency")]
    public string? Currency { get; set; }

    /// <summary>Whether to include archived payment requests.</summary>
    [AliasAs("include_archive")]
    public string? IncludeArchive { get; set; }

    /// <summary>Filter to requests created on or after this timestamp.</summary>
    [AliasAs("from")]
    public DateTimeOffset? From { get; set; }

    /// <summary>Filter to requests created on or before this timestamp.</summary>
    [AliasAs("to")]
    public DateTimeOffset? To { get; set; }
}
