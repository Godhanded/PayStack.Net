namespace PayStack.Net.Models.Common;

/// <summary>
/// Customer address details required when initializing a direct debit mandate — shared by
/// <see cref="Requests.InitializeCustomerAuthorizationRequest"/> and <see cref="Requests.InitializeDirectDebitRequest"/>.
/// </summary>
public sealed class DirectDebitAddressDetails
{
    public required string Street { get; set; }

    public required string City { get; set; }

    public required string State { get; set; }
}
