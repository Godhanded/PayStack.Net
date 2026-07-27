namespace PayStack.Net.Models.Common;

/// <summary>
/// Pagination and diagnostic metadata attached to Paystack responses. Paystack uses two pagination
/// styles depending on the endpoint: offset-based (<see cref="Total"/>/<see cref="Page"/>/<see cref="PerPage"/>/<see cref="PageCount"/>)
/// and cursor-based (<see cref="Next"/>/<see cref="Previous"/>). Only the fields relevant to the
/// endpoint you called will be populated; the rest are <c>null</c>.
/// </summary>
public sealed class PayStackMeta
{
    /// <summary>Total number of records matching the query, across all pages.</summary>
    public int? Total { get; set; }

    /// <summary>Number of records skipped to reach the current page (offset pagination).</summary>
    public int? Skipped { get; set; }

    /// <summary>Number of records returned per page.</summary>
    public int? PerPage { get; set; }

    /// <summary>The current page number (offset pagination, 1-based).</summary>
    public int? Page { get; set; }

    /// <summary>Total number of pages available (offset pagination).</summary>
    public int? PageCount { get; set; }

    /// <summary>Cursor to pass as <c>next</c> to fetch the next page (cursor pagination).</summary>
    public string? Next { get; set; }

    /// <summary>Cursor to pass as <c>previous</c> to fetch the previous page (cursor pagination).</summary>
    public string? Previous { get; set; }

    /// <summary>Diagnostic type on failed requests (e.g. <c>api_error</c>, <c>validation_error</c>), when present.</summary>
    public string? NextStep { get; set; }
}
