using PayStack.Net.Models.Common;
using PayStack.Net.Models.Requests;
using PayStack.Net.Models.Responses;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Terminal API: build POS-integrated payment flows by pushing events to physical Paystack
/// terminals and managing their lifecycle.
/// See <see href="https://paystack.com/docs/api/terminal/">Paystack API reference — Terminal</see>.
/// </summary>
public interface ITerminalClient
{
    /// <summary>Sends an invoice or transaction event to a terminal for it to act on.</summary>
    /// <param name="terminalId">The target terminal's id.</param>
    /// <param name="request">The event type, action, and invoice/transaction payload.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/terminal/{terminalId}/event")]
    Task<ApiResponse<PayStackResponse<TerminalEventData>>> SendEventAsync(
        string terminalId,
        [Body] SendTerminalEventRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Checks whether a previously sent terminal event was delivered.</summary>
    /// <param name="terminalId">The terminal's id.</param>
    /// <param name="eventId">The event id returned by <see cref="SendEventAsync"/>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/terminal/{terminalId}/event/{eventId}")]
    Task<ApiResponse<PayStackResponse<TerminalEventStatusData>>> FetchEventStatusAsync(
        string terminalId,
        string eventId,
        CancellationToken cancellationToken = default);

    /// <summary>Checks whether a terminal is online and available to receive events.</summary>
    /// <param name="terminalId">The terminal's id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/terminal/{terminalId}/presence")]
    Task<ApiResponse<PayStackResponse<TerminalPresenceData>>> FetchTerminalStatusAsync(
        string terminalId,
        CancellationToken cancellationToken = default);

    /// <summary>Lists the terminals on your integration. Uses cursor-based pagination.</summary>
    /// <param name="perPage">Records per page. Defaults to 50.</param>
    /// <param name="next">Cursor to fetch the next page, from a previous response's <c>meta.next</c>.</param>
    /// <param name="previous">Cursor to fetch the previous page, from a previous response's <c>meta.previous</c>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/terminal")]
    Task<ApiResponse<PayStackResponse<List<TerminalData>>>> ListAsync(
        [Query] int? perPage = null,
        [Query] string? next = null,
        [Query] string? previous = null,
        CancellationToken cancellationToken = default);

    /// <summary>Fetches a single terminal by its id.</summary>
    /// <param name="terminalId">The terminal's id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/terminal/{terminalId}")]
    Task<ApiResponse<PayStackResponse<TerminalData>>> FetchAsync(
        string terminalId,
        CancellationToken cancellationToken = default);

    /// <summary>Updates a terminal's name and/or address. Returns no data payload on success.</summary>
    /// <param name="terminalId">The terminal's id.</param>
    /// <param name="request">The new name and/or address.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Put("/terminal/{terminalId}")]
    Task<ApiResponse<PayStackResponse<object>>> UpdateAsync(
        string terminalId,
        [Body] UpdateTerminalRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Links a terminal device to your integration by its serial number.</summary>
    /// <param name="request">The device's serial number.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/terminal/commission_device")]
    Task<ApiResponse<PayStackResponse<object>>> CommissionAsync(
        [Body] CommissionTerminalRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Unlinks a terminal device from your integration by its serial number.</summary>
    /// <param name="request">The device's serial number.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/terminal/decommission_device")]
    Task<ApiResponse<PayStackResponse<object>>> DecommissionAsync(
        [Body] DecommissionTerminalRequest request,
        CancellationToken cancellationToken = default);
}
