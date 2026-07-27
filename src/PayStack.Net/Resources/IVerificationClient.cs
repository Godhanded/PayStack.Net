using PayStack.Net.Models.Common;
using PayStack.Net.Models.Requests;
using PayStack.Net.Models.Responses;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Verification API: resolve/validate bank accounts and look up card BIN metadata.
/// See <see href="https://paystack.com/docs/api/verification/">Paystack API reference — Verification</see>.
/// </summary>
public interface IVerificationClient
{
    /// <summary>Resolves a bank account number to the account holder's name.</summary>
    /// <param name="accountNumber">The bank account number to resolve.</param>
    /// <param name="bankCode">The bank code the account belongs to.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/bank/resolve")]
    Task<ApiResponse<PayStackResponse<ResolvedAccountData>>> ResolveAccountAsync(
        [Query] string accountNumber,
        [Query] string bankCode,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Validates that a bank account belongs to the named holder, cross-checked against a supplied
    /// identity document. Available for a subset of countries/banks.
    /// </summary>
    /// <param name="request">Account, holder, and identity document details to validate.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Post("/bank/validate")]
    Task<ApiResponse<PayStackResponse<ValidatedAccountData>>> ValidateAccountAsync(
        [Body] ValidateAccountRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Resolves card metadata (brand, issuing bank, country) from the first 6 digits of a card number.</summary>
    /// <param name="bin">The first 6 digits of the card number.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/decision/bin/{bin}")]
    Task<ApiResponse<PayStackResponse<CardBinData>>> ResolveCardBinAsync(
        string bin,
        CancellationToken cancellationToken = default);
}
