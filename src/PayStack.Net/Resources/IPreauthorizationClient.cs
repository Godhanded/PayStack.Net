using PayStack.Net.Models.Common;
using PayStack.Net.Models.Requests;
using PayStack.Net.Models.Responses;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Preauthorization API (South Africa / ZAR only): reserve an amount against a customer's
/// authorization now, then capture or release it later, instead of charging immediately.
/// See <see href="https://paystack.com/docs/api/preauthorization/">Paystack API reference — Preauthorization</see>.
/// </summary>
public interface IPreauthorizationClient
{
    /// <summary>Starts a hosted flow to collect and reserve authorization for a future capture.</summary>
    /// <param name="request">Amount, email, currency ("ZAR"), and optional expiry/split configuration.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/preauthorization/initialize")]
    Task<ApiResponse<PayStackResponse<InitializePreauthorizationData>>> InitializeAsync(
        [Body] InitializePreauthorizationRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Captures a previously reserved preauthorization, actually moving the funds.</summary>
    /// <param name="request">Reference, currency, and amount to capture.</param>
    /// <param name="idempotencyKey">
    /// Optional idempotency key. When omitted and <c>PayStackOptions.AutoGenerateIdempotencyKeys</c>
    /// is enabled (default), one is generated automatically so a network-retried request can't double-capture.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/preauthorization/capture")]
    Task<ApiResponse<PayStackResponse<PreauthChargeData>>> CaptureAsync(
        [Body] CapturePreauthorizationRequest request,
        [Header("Idempotency-Key")] string? idempotencyKey = null,
        CancellationToken cancellationToken = default);

    /// <summary>Reserves an amount against an existing reusable authorization for later capture or release.</summary>
    /// <param name="request">Email, amount, authorization code, and currency ("ZAR").</param>
    /// <param name="idempotencyKey">
    /// Optional idempotency key. When omitted and <c>PayStackOptions.AutoGenerateIdempotencyKeys</c>
    /// is enabled (default), one is generated automatically so a network-retried request can't create a duplicate reservation.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/preauthorization/reserve_authorization")]
    Task<ApiResponse<PayStackResponse<PreauthorizationReservationData>>> ReserveAuthorizationAsync(
        [Body] ReservePreauthorizationRequest request,
        [Header("Idempotency-Key")] string? idempotencyKey = null,
        CancellationToken cancellationToken = default);

    /// <summary>Verifies the current state of a preauthorization reservation, including whether it has since been captured.</summary>
    /// <param name="reference">The reservation's reference.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/preauthorization/verify/{reference}")]
    Task<ApiResponse<PayStackResponse<PreauthorizationReservationData>>> VerifyAsync(
        string reference,
        CancellationToken cancellationToken = default);

    /// <summary>Releases a reserved preauthorization without charging it.</summary>
    /// <param name="request">The reference of the reservation to release.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/preauthorization/release")]
    Task<ApiResponse<PayStackResponse<PreauthorizationReleaseData>>> ReleaseAsync(
        [Body] ReleasePreauthorizationRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Lists preauthorization reservations on your integration.</summary>
    /// <param name="perPage">Records per page.</param>
    /// <param name="page">Page number.</param>
    /// <param name="customer">Filter by Paystack customer id.</param>
    /// <param name="status">Filter by status. See <see cref="PayStackPreauthorizationStatus"/>.</param>
    /// <param name="from">Filter to reservations created on or after this timestamp.</param>
    /// <param name="to">Filter to reservations created on or before this timestamp.</param>
    /// <param name="amount">Filter by exact amount, in subunits.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/preauthorization")]
    Task<ApiResponse<PayStackResponse<List<PreauthorizationListItem>>>> ListAsync(
        [Query] int? perPage = null,
        [Query] int? page = null,
        [Query] long? customer = null,
        [Query] string? status = null,
        [Query] DateTimeOffset? from = null,
        [Query] DateTimeOffset? to = null,
        [Query] long? amount = null,
        CancellationToken cancellationToken = default);
}
