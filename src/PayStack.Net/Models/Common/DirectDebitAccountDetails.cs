namespace PayStack.Net.Models.Common;

/// <summary>
/// Bank account details supplied when initializing a direct debit mandate — shared by
/// <see cref="Requests.InitializeCustomerAuthorizationRequest"/> and <see cref="Requests.InitializeDirectDebitRequest"/>.
/// </summary>
public sealed class DirectDebitAccountDetails
{
    /// <summary>Bank account number.</summary>
    public required string Number { get; set; }

    /// <summary>Paystack bank code for the account's bank.</summary>
    public required string BankCode { get; set; }
}
