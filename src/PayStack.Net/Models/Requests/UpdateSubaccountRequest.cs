namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>PUT /subaccount/:id_or_code</c>.</summary>
public sealed class UpdateSubaccountRequest
{
    /// <summary>Name of the business associated with this subaccount. Required.</summary>
    public required string BusinessName { get; set; }

    /// <summary>Required by Paystack's docs even when unchanged.</summary>
    public required string Description { get; set; }

    /// <summary>Bank code of the settlement bank, as returned by the Miscellaneous "list banks" endpoint.</summary>
    public string? BankCode { get; set; }

    public string? AccountNumber { get; set; }

    public bool? Active { get; set; }

    /// <summary>Percentage of each transaction that should go to this subaccount.</summary>
    public double? PercentageCharge { get; set; }

    public string? PrimaryContactEmail { get; set; }

    public string? PrimaryContactName { get; set; }

    public string? PrimaryContactPhone { get; set; }

    /// <summary>How often this subaccount should be settled. See <see cref="Common.PayStackSettlementSchedule"/>. Defaults to "auto".</summary>
    public string? SettlementSchedule { get; set; }

    /// <summary>Stringified JSON object containing a <c>custom_fields</c> array, or other arbitrary metadata.</summary>
    public string? Metadata { get; set; }
}
