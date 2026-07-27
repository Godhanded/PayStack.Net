namespace PayStack.Net.Models.Requests;

/// <summary>
/// Request body for <c>POST /dedicated_account/assign</c>. This creates a customer (if one doesn't
/// already exist with the given email), validates their details, and assigns a dedicated virtual
/// account, all in one call. The operation is asynchronous — the response only confirms the request
/// was accepted; the actual assignment result is delivered via webhook.
/// </summary>
public sealed class AssignDedicatedVirtualAccountRequest
{
    public required string Email { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Phone { get; set; }

    /// <summary>The bank slug to create the dedicated account with.</summary>
    public required string PreferredBank { get; set; }

    /// <summary>2-letter country code the dedicated account should be created in, e.g. "NG", "GH".</summary>
    public required string Country { get; set; }

    public string? AccountNumber { get; set; }

    /// <summary>Bank Verification Number. Required for Nigerian customers depending on your integration's settings.</summary>
    public string? Bvn { get; set; }

    public string? BankCode { get; set; }

    /// <summary>Subaccount code that transactions on this dedicated account should be split with.</summary>
    public string? Subaccount { get; set; }

    /// <summary>Split code of a pre-created transaction split to apply to transactions on this dedicated account.</summary>
    public string? SplitCode { get; set; }
}
