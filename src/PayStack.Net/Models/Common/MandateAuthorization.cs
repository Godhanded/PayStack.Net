namespace PayStack.Net.Models.Common;

/// <summary>
/// A direct debit mandate authorization: the link between a customer's bank account and Paystack's
/// permission to debit it. Returned by both <see cref="Resources.ICustomersClient.FetchMandateAuthorizationsAsync"/>
/// and <see cref="Resources.IDirectDebitClient.ListMandateAuthorizationsAsync"/>.
/// </summary>
public sealed class MandateAuthorization
{
    public long Id { get; set; }

    /// <summary>Mandate status. See <see cref="PayStackMandateStatus"/> for known values.</summary>
    public string Status { get; set; } = string.Empty;

    public string? MandateId { get; set; }

    public long AuthorizationId { get; set; }

    public string? AuthorizationCode { get; set; }

    public long IntegrationId { get; set; }

    public string? AccountNumber { get; set; }

    public string? BankCode { get; set; }

    public string? BankName { get; set; }

    public MandateAuthorizationCustomer? Customer { get; set; }

    public DateTimeOffset? AuthorizedAt { get; set; }
}

/// <summary>The abbreviated customer object embedded in a <see cref="MandateAuthorization"/>.</summary>
public sealed class MandateAuthorizationCustomer
{
    public long Id { get; set; }

    public string CustomerCode { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }
}
