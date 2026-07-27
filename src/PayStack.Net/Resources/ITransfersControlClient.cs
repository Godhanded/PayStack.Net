using PayStack.Net.Models.Common;
using PayStack.Net.Models.Requests;
using PayStack.Net.Models.Responses;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Transfers Control API: check your balance, view balance movements, and manage the OTP
/// requirement Paystack enforces on outbound transfers.
/// See <see href="https://paystack.com/docs/api/transfer-control/">Paystack API reference — Transfers Control</see>.
/// </summary>
public interface ITransfersControlClient
{
    /// <summary>Retrieves the available balance on your integration, per currency.</summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/balance")]
    Task<ApiResponse<PayStackResponse<List<BalanceData>>>> CheckBalanceAsync(
        CancellationToken cancellationToken = default);

    /// <summary>Retrieves the history of balance movements (a ledger of debits and credits) on your integration.</summary>
    /// <param name="perPage">Records per page.</param>
    /// <param name="page">Page number.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/balance/ledger")]
    Task<ApiResponse<PayStackResponse<List<BalanceLedgerEntryData>>>> FetchBalanceLedgerAsync(
        [Query] int? perPage = null,
        [Query] int? page = null,
        CancellationToken cancellationToken = default);

    /// <summary>Generates a new OTP and sends it to the transfer's recipient/account holder, for a transfer awaiting finalization.</summary>
    /// <param name="request">The transfer code and the reason for resending.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/transfer/resend_otp")]
    Task<ApiResponse<PayStackResponse<object?>>> ResendOtpAsync(
        [Body] ResendTransferOtpRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Begins disabling the Transfers OTP requirement on your integration. This sends an OTP to your
    /// registered contact, which must then be submitted via <see cref="FinalizeDisableOtpAsync"/> to
    /// complete the change. Disabling OTP allows transfers to complete without per-transfer confirmation —
    /// understand the payout-safety implications before calling this.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/transfer/disable_otp")]
    Task<ApiResponse<PayStackResponse<object?>>> DisableOtpAsync(
        CancellationToken cancellationToken = default);

    /// <summary>Completes disabling the Transfers OTP requirement using the OTP sent by <see cref="DisableOtpAsync"/>.</summary>
    /// <param name="request">The OTP received.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/transfer/disable_otp_finalize")]
    Task<ApiResponse<PayStackResponse<object?>>> FinalizeDisableOtpAsync(
        [Body] FinalizeDisableTransferOtpRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Re-enables the Transfers OTP requirement on your integration.</summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/transfer/enable_otp")]
    Task<ApiResponse<PayStackResponse<object?>>> EnableOtpAsync(
        CancellationToken cancellationToken = default);
}
