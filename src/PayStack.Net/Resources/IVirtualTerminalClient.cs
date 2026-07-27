using PayStack.Net.Models.Common;
using PayStack.Net.Models.Requests;
using PayStack.Net.Models.Responses;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Virtual Terminal API: accept payments over WhatsApp by creating shareable payment
/// destinations, optionally split via a transaction split code.
/// See <see href="https://paystack.com/docs/api/virtual-terminal/">Paystack API reference — Virtual Terminal</see>.
/// </summary>
public interface IVirtualTerminalClient
{
    /// <summary>Creates a new virtual terminal.</summary>
    /// <param name="request">Name, WhatsApp destinations, and optional currency/metadata/custom fields.</param>
    /// <param name="idempotencyKey">
    /// Optional idempotency key. When omitted and <c>PayStackOptions.AutoGenerateIdempotencyKeys</c>
    /// is enabled (default), one is generated automatically so a network-retried request can't create duplicates.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/virtual_terminal")]
    Task<ApiResponse<PayStackResponse<VirtualTerminalData>>> CreateAsync(
        [Body] CreateVirtualTerminalRequest request,
        [Header("Idempotency-Key")] string? idempotencyKey = null,
        CancellationToken cancellationToken = default);

    /// <summary>Lists virtual terminals on your integration. Uses cursor-based pagination.</summary>
    /// <param name="status">Filter by status. See <see cref="PayStackVirtualTerminalStatus"/> for known values.</param>
    /// <param name="perPage">Records per page.</param>
    /// <param name="search">Free-text search over virtual terminal names.</param>
    /// <param name="next">Cursor to fetch the next page, from a previous response's <c>meta.next</c>.</param>
    /// <param name="previous">Cursor to fetch the previous page, from a previous response's <c>meta.previous</c>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/virtual_terminal")]
    Task<ApiResponse<PayStackResponse<List<VirtualTerminalData>>>> ListAsync(
        [Query] string? status = null,
        [Query] int? perPage = null,
        [Query] string? search = null,
        [Query] string? next = null,
        [Query] string? previous = null,
        CancellationToken cancellationToken = default);

    /// <summary>Fetches a single virtual terminal by its code.</summary>
    /// <param name="code">The virtual terminal's code.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/virtual_terminal/{code}")]
    Task<ApiResponse<PayStackResponse<VirtualTerminalData>>> FetchAsync(
        string code,
        CancellationToken cancellationToken = default);

    /// <summary>Renames a virtual terminal.</summary>
    /// <param name="code">The virtual terminal's code.</param>
    /// <param name="request">The new name.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Put("/virtual_terminal/{code}")]
    Task<ApiResponse<PayStackResponse<VirtualTerminalData>>> UpdateAsync(
        string code,
        [Body] UpdateVirtualTerminalRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Deactivates a virtual terminal, stopping it from accepting further payments. Returns no data payload.</summary>
    /// <param name="code">The virtual terminal's code.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Put("/virtual_terminal/{code}/deactivate")]
    Task<ApiResponse<PayStackResponse<object>>> DeactivateAsync(
        string code,
        CancellationToken cancellationToken = default);

    /// <summary>Assigns one or more WhatsApp destinations to a virtual terminal.</summary>
    /// <param name="code">The virtual terminal's code.</param>
    /// <param name="request">The destinations to assign.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/virtual_terminal/{code}/destination/assign")]
    Task<ApiResponse<PayStackResponse<List<VirtualTerminalDestinationAssignData>>>> AssignDestinationAsync(
        string code,
        [Body] AssignVirtualTerminalDestinationRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Removes one or more WhatsApp destinations from a virtual terminal. Returns no data payload.</summary>
    /// <param name="code">The virtual terminal's code.</param>
    /// <param name="request">The destination targets (phone numbers) to remove.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/virtual_terminal/{code}/destination/unassign")]
    Task<ApiResponse<PayStackResponse<object>>> UnassignDestinationAsync(
        string code,
        [Body] UnassignVirtualTerminalDestinationRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Attaches a transaction split code to a virtual terminal, splitting proceeds between recipients.</summary>
    /// <param name="code">The virtual terminal's code.</param>
    /// <param name="request">The split code to apply.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Put("/virtual_terminal/{code}/split_code")]
    Task<ApiResponse<PayStackResponse<VirtualTerminalSplitData>>> AddSplitCodeAsync(
        string code,
        [Body] AddVirtualTerminalSplitCodeRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Removes the transaction split code from a virtual terminal. Returns no data payload.</summary>
    /// <param name="code">The virtual terminal's code.</param>
    /// <param name="request">The split code to remove.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Delete("/virtual_terminal/{code}/split_code")]
    Task<ApiResponse<PayStackResponse<object>>> RemoveSplitCodeAsync(
        string code,
        [Body] RemoveVirtualTerminalSplitCodeRequest request,
        CancellationToken cancellationToken = default);
}
