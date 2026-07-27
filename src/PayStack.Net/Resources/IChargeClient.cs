using PayStack.Net.Models.Common;
using PayStack.Net.Models.Requests;
using PayStack.Net.Models.Responses;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Charge API: charge a customer directly on a specific channel (bank, USSD, mobile money, QR,
/// EFT, Capitec Pay) or via a stored authorization, without a hosted checkout redirect. A charge can
/// pause mid-flight requesting more information (PIN, OTP, phone, birthday, address) — submit it via
/// the matching <c>submit_*</c> method and re-check <c>data.status</c> until it resolves.
/// See <see href="https://paystack.com/docs/api/charge/">Paystack API reference — Charge</see>.
/// </summary>
public interface IChargeClient
{
    /// <summary>
    /// Initiates a direct charge. Always returns HTTP 200; branch on <c>data.status</c>
    /// (see <see cref="PayStackChargeStatus"/>) — a non-"success" status may require calling one of
    /// the <c>submit_*</c> methods below, or polling <see cref="CheckPendingChargeAsync"/>.
    /// </summary>
    /// <param name="request">Email, amount, and exactly one of a channel-specific detail object or an authorization code.</param>
    /// <param name="idempotencyKey">
    /// Optional idempotency key. When omitted and <c>PayStackOptions.AutoGenerateIdempotencyKeys</c>
    /// is enabled (default), one is generated automatically so a network-retried request can't double-charge.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/charge")]
    Task<ApiResponse<PayStackResponse<ChargeData>>> CreateAsync(
        [Body] CreateChargeRequest request,
        [Header("Idempotency-Key")] string? idempotencyKey = null,
        CancellationToken cancellationToken = default);

    /// <summary>Continues a pending charge that returned status "send_pin" by submitting the card PIN.</summary>
    /// <param name="request">The PIN and the pending charge's reference.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/charge/submit_pin")]
    Task<ApiResponse<PayStackResponse<ChargeData>>> SubmitPinAsync(
        [Body] SubmitPinRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Continues a pending charge that returned status "send_otp" by submitting the one-time-pin.</summary>
    /// <param name="request">The OTP and the pending charge's reference.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/charge/submit_otp")]
    Task<ApiResponse<PayStackResponse<ChargeData>>> SubmitOtpAsync(
        [Body] SubmitOtpRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Continues a pending charge that returned status "send_phone" by submitting the customer's phone number.</summary>
    /// <param name="request">The phone number and the pending charge's reference.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/charge/submit_phone")]
    Task<ApiResponse<PayStackResponse<ChargeData>>> SubmitPhoneAsync(
        [Body] SubmitPhoneRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Continues a pending charge that returned status "send_birthday" by submitting the customer's date of birth.</summary>
    /// <param name="request">The birthday and the pending charge's reference.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/charge/submit_birthday")]
    Task<ApiResponse<PayStackResponse<ChargeData>>> SubmitBirthdayAsync(
        [Body] SubmitBirthdayRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Continues a pending charge that returned status "send_address" by submitting the customer's billing address.</summary>
    /// <param name="request">The address fields and the pending charge's reference.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/charge/submit_address")]
    Task<ApiResponse<PayStackResponse<ChargeData>>> SubmitAddressAsync(
        [Body] SubmitAddressRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks the current status of a pending charge. Wait at least 10 seconds after a
    /// pending/timeout result before calling this — tight polling can trip rate limits.
    /// </summary>
    /// <param name="reference">The charge's reference.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/charge/{reference}")]
    Task<ApiResponse<PayStackResponse<ChargeData>>> CheckPendingChargeAsync(
        string reference,
        CancellationToken cancellationToken = default);
}
