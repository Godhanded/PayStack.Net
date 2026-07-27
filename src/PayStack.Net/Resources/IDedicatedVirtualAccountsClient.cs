using PayStack.Net.Models.Common;
using PayStack.Net.Models.Requests;
using PayStack.Net.Models.Responses;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Dedicated Virtual Accounts API: create and manage unique, customer-specific bank accounts
/// (NUBAN/GHIPSS) that route incoming bank transfers straight to a Paystack customer.
/// See <see href="https://paystack.com/docs/api/dedicated-virtual-account/">Paystack API reference — Dedicated Virtual Accounts</see>.
/// </summary>
public interface IDedicatedVirtualAccountsClient
{
    /// <summary>Creates a dedicated virtual account for an existing customer.</summary>
    /// <param name="request">The customer to attach the account to, plus optional bank/split preferences.</param>
    /// <param name="idempotencyKey">
    /// Optional idempotency key. When omitted and <c>PayStackOptions.AutoGenerateIdempotencyKeys</c>
    /// is enabled (default), one is generated automatically so a network-retried request can't create duplicate accounts.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/dedicated_account")]
    Task<ApiResponse<PayStackResponse<DedicatedVirtualAccountData>>> CreateAsync(
        [Body] CreateDedicatedVirtualAccountRequest request,
        [Header("Idempotency-Key")] string? idempotencyKey = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a customer (if one doesn't already exist), validates their details, and assigns a
    /// dedicated virtual account in one call. This is asynchronous — the response only acknowledges
    /// the request; the actual assignment result is delivered via webhook.
    /// </summary>
    /// <param name="request">Customer details, preferred bank, and country to assign the account in.</param>
    /// <param name="idempotencyKey">Optional idempotency key; auto-generated when omitted (see <see cref="CreateAsync"/>).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/dedicated_account/assign")]
    Task<ApiResponse<PayStackResponse<object>>> AssignAsync(
        [Body] AssignDedicatedVirtualAccountRequest request,
        [Header("Idempotency-Key")] string? idempotencyKey = null,
        CancellationToken cancellationToken = default);

    /// <summary>Lists dedicated virtual accounts on your integration.</summary>
    /// <param name="active">Filter by active status.</param>
    /// <param name="currency">Filter by currency. See <see cref="PayStackDedicatedAccountCurrency"/>.</param>
    /// <param name="providerSlug">Filter by bank provider slug. Required if <paramref name="bankId"/> is provided.</param>
    /// <param name="bankId">Filter by bank id. Required if <paramref name="providerSlug"/> is provided.</param>
    /// <param name="customer">Filter by Paystack customer id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/dedicated_account")]
    Task<ApiResponse<PayStackResponse<List<DedicatedVirtualAccountData>>>> ListAsync(
        [Query] bool? active = null,
        [Query] string? currency = null,
        [Query] string? providerSlug = null,
        [Query] long? bankId = null,
        [Query] long? customer = null,
        CancellationToken cancellationToken = default);

    /// <summary>Fetches a single dedicated virtual account by its Paystack id.</summary>
    /// <param name="dedicatedAccountId">The dedicated virtual account's numeric id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/dedicated_account/{dedicatedAccountId}")]
    Task<ApiResponse<PayStackResponse<DedicatedVirtualAccountData>>> FetchAsync(
        long dedicatedAccountId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Requeries a dedicated virtual account for new transfers. Useful when a customer says they've
    /// paid but the transaction hasn't reflected yet. Returns no <c>data</c> payload — check <c>message</c>.
    /// </summary>
    /// <param name="accountNumber">The dedicated account number to requery.</param>
    /// <param name="providerSlug">The bank provider slug the account belongs to. Required.</param>
    /// <param name="date">Date (YYYY-MM-DD) to requery for, when different from today.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/dedicated_account/requery")]
    Task<ApiResponse<PayStackResponse<object>>> RequeryAsync(
        [Query] string accountNumber,
        [Query] string providerSlug,
        [Query] string? date = null,
        CancellationToken cancellationToken = default);

    /// <summary>Deactivates (unassigns) a dedicated virtual account.</summary>
    /// <param name="dedicatedAccountId">The dedicated virtual account's numeric id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Delete("/dedicated_account/{dedicatedAccountId}")]
    Task<ApiResponse<PayStackResponse<DedicatedVirtualAccountData>>> DeactivateAsync(
        long dedicatedAccountId,
        CancellationToken cancellationToken = default);

    /// <summary>Splits a dedicated virtual account's transactions with a subaccount or split code.</summary>
    /// <param name="request">The customer and split configuration to apply.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/dedicated_account/split")]
    Task<ApiResponse<PayStackResponse<DedicatedVirtualAccountData>>> SplitTransactionAsync(
        [Body] SplitDedicatedVirtualAccountRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Removes a subaccount/split code from a dedicated virtual account.</summary>
    /// <param name="request">The account number to remove the split from.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Delete("/dedicated_account/split")]
    Task<ApiResponse<PayStackResponse<DedicatedVirtualAccountData>>> RemoveSplitAsync(
        [Body] RemoveDedicatedVirtualAccountSplitRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Fetches the list of banks that support dedicated virtual accounts.</summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/dedicated_account/available_providers")]
    Task<ApiResponse<PayStackResponse<List<DedicatedVirtualAccountProvider>>>> FetchBankProvidersAsync(
        CancellationToken cancellationToken = default);
}
