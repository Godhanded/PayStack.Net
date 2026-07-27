using PayStack.Net.Models.Common;
using PayStack.Net.Models.Responses;
using Refit;

namespace PayStack.Net.Resources;

/// <summary>
/// The Miscellaneous API: reference/lookup data used to power dropdowns and validation elsewhere
/// in your integration — supported banks, countries, and AVS states.
/// See <see href="https://paystack.com/docs/api/miscellaneous/">Paystack API reference — Miscellaneous</see>.
/// </summary>
public interface IMiscellaneousClient
{
    /// <summary>Lists the banks supported by Paystack for a given country.</summary>
    /// <param name="country">The country to list banks for. Required. See <see cref="PayStackBankCountry"/>.</param>
    /// <param name="useCursor">Whether to paginate using cursors.</param>
    /// <param name="perPage">Records per page. Defaults to 50, max 100.</param>
    /// <param name="payWithBankTransfer">Filter to banks that support pay-with-bank-transfer.</param>
    /// <param name="payWithBank">Filter to banks that support pay-with-bank.</param>
    /// <param name="enabledForVerification">Filter to banks enabled for identity verification. Combine with <paramref name="currency"/>/<paramref name="country"/>.</param>
    /// <param name="next">Cursor to fetch the next page.</param>
    /// <param name="previous">Cursor to fetch the previous page.</param>
    /// <param name="gateway">Filter by gateway. See <see cref="PayStackBankGateway"/>.</param>
    /// <param name="type">Filter by bank type, e.g. "mobile_money", "ghipss" (Ghana only).</param>
    /// <param name="currency">Filter by ISO 4217 currency code.</param>
    /// <param name="includeNipSortCode">Whether to include the NIP sort code for Nigerian banks.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/bank")]
    Task<ApiResponse<PayStackResponse<List<BankData>>>> ListBanksAsync(
        [Query] string country,
        [Query] bool? useCursor = null,
        [Query] int? perPage = null,
        [Query] bool? payWithBankTransfer = null,
        [Query] bool? payWithBank = null,
        [Query] bool? enabledForVerification = null,
        [Query] string? next = null,
        [Query] string? previous = null,
        [Query] string? gateway = null,
        [Query] string? type = null,
        [Query] string? currency = null,
        [Query] bool? includeNipSortCode = null,
        CancellationToken cancellationToken = default);

    /// <summary>Lists countries supported by Paystack, and their integration defaults.</summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/country")]
    Task<ApiResponse<PayStackResponse<List<CountryData>>>> ListCountriesAsync(
        CancellationToken cancellationToken = default);

    /// <summary>Lists states/provinces for a country, for use with address verification (AVS).</summary>
    /// <param name="country">2-letter country code to list states for. Required. See <see cref="PayStackAvsCountry"/>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [Get("/address_verification/states")]
    Task<ApiResponse<PayStackResponse<List<AvsStateData>>>> ListAvsStatesAsync(
        [Query] string country,
        CancellationToken cancellationToken = default);
}
