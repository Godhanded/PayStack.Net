using PayStack.Net.Models.Common;

namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /customer/:id/initialize-direct-debit</c> — starts direct debit mandate setup for a specific customer.</summary>
public sealed class InitializeDirectDebitRequest
{
    /// <summary>Bank account to link. Required.</summary>
    public required DirectDebitAccountDetails Account { get; set; }

    /// <summary>Customer address. Required.</summary>
    public required DirectDebitAddressDetails Address { get; set; }
}
