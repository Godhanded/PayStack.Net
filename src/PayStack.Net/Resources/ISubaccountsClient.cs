using PayStack.Net.Models.Common;
using PayStack.Net.Models.Requests;
using PayStack.Net.Models.Responses;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Subaccounts API: create and manage subaccounts used to split transaction proceeds between
/// your integration and a business/vendor. See
/// <see href="https://paystack.com/docs/api/subaccount/">Paystack API reference — Subaccounts</see>.
/// </summary>
public interface ISubaccountsClient
{
    /// <summary>Creates a subaccount that can later be used to split transaction proceeds.</summary>
    /// <param name="request">Business, settlement bank, and split percentage details.</param>
    /// <param name="idempotencyKey">
    /// Optional idempotency key. When omitted and <c>PayStackOptions.AutoGenerateIdempotencyKeys</c>
    /// is enabled (default), one is generated automatically so a network-retried request can't create duplicate subaccounts.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/subaccount")]
    Task<ApiResponse<PayStackResponse<SubaccountData>>> CreateAsync(
        [Body] CreateSubaccountRequest request,
        [Header("Idempotency-Key")] string? idempotencyKey = null,
        CancellationToken cancellationToken = default);

    /// <summary>Lists subaccounts on your integration.</summary>
    /// <param name="perPage">Records per page. Defaults to 50.</param>
    /// <param name="page">Page number. Defaults to 1.</param>
    /// <param name="from">Filter to subaccounts created on or after this timestamp.</param>
    /// <param name="to">Filter to subaccounts created on or before this timestamp.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/subaccount")]
    Task<ApiResponse<PayStackResponse<List<SubaccountData>>>> ListAsync(
        [Query] int? perPage = null,
        [Query] int? page = null,
        [Query] DateTimeOffset? from = null,
        [Query] DateTimeOffset? to = null,
        CancellationToken cancellationToken = default);

    /// <summary>Fetches a single subaccount by its numeric id or its subaccount code.</summary>
    /// <param name="idOrCode">The subaccount's numeric id or its "ACCT_xxx" code.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/subaccount/{idOrCode}")]
    Task<ApiResponse<PayStackResponse<SubaccountData>>> FetchAsync(
        string idOrCode,
        CancellationToken cancellationToken = default);

    /// <summary>Updates a subaccount's details.</summary>
    /// <param name="idOrCode">The subaccount's numeric id or its "ACCT_xxx" code.</param>
    /// <param name="request">Fields to update. Paystack requires <c>business_name</c> and <c>description</c> on every update, even if unchanged.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Put("/subaccount/{idOrCode}")]
    Task<ApiResponse<PayStackResponse<SubaccountData>>> UpdateAsync(
        string idOrCode,
        [Body] UpdateSubaccountRequest request,
        CancellationToken cancellationToken = default);
}
