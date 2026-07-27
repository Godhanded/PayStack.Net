namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /subaccount</c>.</summary>
public sealed class CreateSubaccountRequest
{
    /// <summary>Name of the business associated with this subaccount. Required.</summary>
    public required string BusinessName { get; set; }

    /// <summary>Bank code of the settlement bank, as returned by the Miscellaneous "list banks" endpoint. Required.</summary>
    public required string BankCode { get; set; }

    /// <summary>Bank account number to settle into. Required.</summary>
    public required string AccountNumber { get; set; }

    /// <summary>Percentage of each transaction that should go to this subaccount. Required.</summary>
    public required double PercentageCharge { get; set; }

    public string? Description { get; set; }

    public string? PrimaryContactEmail { get; set; }

    public string? PrimaryContactName { get; set; }

    public string? PrimaryContactPhone { get; set; }

    /// <summary>Stringified JSON object containing a <c>custom_fields</c> array, or other arbitrary metadata.</summary>
    public string? Metadata { get; set; }
}
