using PayStack.Net.Models.Common;
using PayStack.Net.Models.Requests;
using PayStack.Net.Models.Responses;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Customers API: create and manage customer records, whitelist/blacklist customers, and set up
/// non-transactional authorizations including direct debit mandates.
/// See <see href="https://paystack.com/docs/api/customer/">Paystack API reference — Customers</see>.
/// </summary>
public interface ICustomersClient
{
    /// <summary>Creates a new customer.</summary>
    /// <param name="request">Email and optional name/phone/metadata.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/customer")]
    Task<ApiResponse<PayStackResponse<CustomerData>>> CreateAsync(
        [Body] CreateCustomerRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Lists customers on your integration.</summary>
    /// <param name="perPage">Records per page. Defaults to 50.</param>
    /// <param name="page">Page number. Defaults to 1.</param>
    /// <param name="from">Filter to customers created on or after this timestamp.</param>
    /// <param name="to">Filter to customers created on or before this timestamp.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/customer")]
    Task<ApiResponse<PayStackResponse<List<CustomerData>>>> ListAsync(
        [Query] int? perPage = null,
        [Query] int? page = null,
        [Query] DateTimeOffset? from = null,
        [Query] DateTimeOffset? to = null,
        CancellationToken cancellationToken = default);

    /// <summary>Fetches a single customer by email or customer code, including their transaction/subscription/authorization history.</summary>
    /// <param name="emailOrCode">The customer's email address or customer code.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/customer/{emailOrCode}")]
    Task<ApiResponse<PayStackResponse<CustomerDetailData>>> FetchAsync(
        string emailOrCode,
        CancellationToken cancellationToken = default);

    /// <summary>Updates a customer's name, phone, or metadata.</summary>
    /// <param name="code">The customer's customer code.</param>
    /// <param name="request">Fields to update.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Put("/customer/{code}")]
    Task<ApiResponse<PayStackResponse<CustomerData>>> UpdateAsync(
        string code,
        [Body] UpdateCustomerRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Kicks off identity validation for a customer (currently only bank account validation is
    /// supported). Asynchronous: Paystack returns HTTP 202 immediately with no <c>data</c> payload
    /// and delivers the result via webhook.
    /// </summary>
    /// <param name="emailOrCode">The customer's email address or customer code.</param>
    /// <param name="request">Identity details to validate.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/customer/{emailOrCode}/identification")]
    Task<ApiResponse<PayStackResponse<object?>>> ValidateAsync(
        string emailOrCode,
        [Body] ValidateCustomerRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Whitelists or blacklists a customer, controlling whether their future charges are automatically allowed/blocked.</summary>
    /// <param name="request">The customer identifier and the risk action to apply.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/customer/set_risk_action")]
    Task<ApiResponse<PayStackResponse<CustomerData>>> SetRiskActionAsync(
        [Body] SetCustomerRiskActionRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Starts a hosted flow to collect a reusable authorization for a customer outside of a transaction (currently direct debit only).</summary>
    /// <param name="request">Email, channel ("direct_debit"), and the bank account/address to link.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/customer/authorization/initialize")]
    Task<ApiResponse<PayStackResponse<CustomerAuthorizationInitializeData>>> InitializeAuthorizationAsync(
        [Body] InitializeCustomerAuthorizationRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Verifies the outcome of an authorization initialized via <see cref="InitializeAuthorizationAsync"/>.</summary>
    /// <param name="reference">The reference returned by <see cref="InitializeAuthorizationAsync"/>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/customer/authorization/verify/{reference}")]
    Task<ApiResponse<PayStackResponse<CustomerAuthorizationVerifyData>>> VerifyAuthorizationAsync(
        string reference,
        CancellationToken cancellationToken = default);

    /// <summary>Starts direct debit mandate setup for a specific existing customer.</summary>
    /// <param name="id">The customer's numeric id.</param>
    /// <param name="request">The bank account and address to link.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/customer/{id}/initialize-direct-debit")]
    Task<ApiResponse<PayStackResponse<CustomerAuthorizationInitializeData>>> InitializeDirectDebitAsync(
        long id,
        [Body] InitializeDirectDebitRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Retries the activation charge for a customer's pending direct debit mandate.</summary>
    /// <param name="id">The customer's numeric id.</param>
    /// <param name="request">The pending mandate authorization's id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Put("/customer/{id}/directdebit-activation-charge")]
    Task<ApiResponse<PayStackResponse<object?>>> TriggerDirectDebitActivationChargeAsync(
        long id,
        [Body] DirectDebitActivationChargeRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Fetches a customer's direct debit mandate authorizations.</summary>
    /// <param name="id">The customer's numeric id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/customer/{id}/directdebit-mandate-authorizations")]
    Task<ApiResponse<PayStackResponse<List<MandateAuthorization>>>> FetchMandateAuthorizationsAsync(
        long id,
        CancellationToken cancellationToken = default);

    /// <summary>Deactivates a reusable authorization so it can no longer be charged.</summary>
    /// <param name="request">The authorization code to deactivate.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/customer/authorization/deactivate")]
    Task<ApiResponse<PayStackResponse<object?>>> DeactivateAuthorizationAsync(
        [Body] DeactivateAuthorizationRequest request,
        CancellationToken cancellationToken = default);
}
