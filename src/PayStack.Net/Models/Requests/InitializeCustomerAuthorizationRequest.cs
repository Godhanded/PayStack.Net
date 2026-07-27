using PayStack.Net.Models.Common;

namespace PayStack.Net.Models.Requests;

/// <summary>
/// Request body for <c>POST /customer/authorization/initialize</c> — starts a flow to collect a
/// reusable authorization for a customer outside of a transaction. Currently only the
/// "direct_debit" channel is supported.
/// </summary>
public sealed class InitializeCustomerAuthorizationRequest
{
    /// <summary>Customer's email address. Required.</summary>
    public required string Email { get; set; }

    /// <summary>Authorization channel. Only "direct_debit" is currently supported. Required.</summary>
    public required string Channel { get; set; }

    public string? CallbackUrl { get; set; }

    /// <summary>Bank account to link, for the "direct_debit" channel.</summary>
    public DirectDebitAccountDetails? Account { get; set; }

    /// <summary>Customer address, for the "direct_debit" channel.</summary>
    public DirectDebitAddressDetails? Address { get; set; }
}
