namespace PayStack.Net.Models.Common;

/// <summary>
/// The standard response envelope returned by every Paystack API endpoint:
/// <c>{ "status": bool, "message": string, "data": ..., "meta": ... }</c>.
/// </summary>
/// <typeparam name="TData">The shape of the <c>data</c> payload for this endpoint (an object or a list).</typeparam>
public sealed class PayStackResponse<TData>
{
    /// <summary>
    /// Whether the request succeeded. Prefer checking this alongside the HTTP status code
    /// (surfaced separately via Refit's <c>ApiResponse&lt;T&gt;.IsSuccessStatusCode</c>) rather than relying on either alone.
    /// </summary>
    public bool Status { get; set; }

    /// <summary>
    /// Human-readable summary of the result, e.g. "Customers retrieved" or a description of what went wrong.
    /// This is the one field Paystack guarantees on every response, success or failure.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>The requested resource (object) or collection (array), or <c>null</c> for endpoints that return no payload.</summary>
    public TData? Data { get; set; }

    /// <summary>
    /// Supplementary information about the response. On list endpoints this carries pagination details
    /// (see <see cref="PayStackMeta"/>); on failed requests it can carry diagnostic details.
    /// </summary>
    public PayStackMeta? Meta { get; set; }
}
