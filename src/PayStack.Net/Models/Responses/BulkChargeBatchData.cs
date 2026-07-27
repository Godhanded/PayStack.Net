namespace PayStack.Net.Models.Responses;

/// <summary>
/// A Paystack bulk charge batch, returned by initiate/list/fetch on the Bulk Charges API.
/// Not every field is populated by every endpoint: <see cref="Reference"/> and <see cref="Integration"/>
/// are only returned by Initiate Bulk Charge; <see cref="TotalCharges"/> and <see cref="PendingCharges"/>
/// are returned by Initiate and Fetch but not List.
/// </summary>
public sealed class BulkChargeBatchData
{
    public string? Domain { get; set; }

    /// <summary>Public identifier for the batch, e.g. "BCH_xxx".</summary>
    public string BatchCode { get; set; } = string.Empty;

    /// <summary>Batch status. See <see cref="Common.PayStackBulkChargeBatchStatus"/>.</summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>Paystack's internal numeric batch id.</summary>
    public long Id { get; set; }

    /// <summary>Initiate Bulk Charge only: the reference of the first charge in the batch.</summary>
    public string? Reference { get; set; }

    public long? Integration { get; set; }

    /// <summary>Initiate and Fetch only: total number of charges in the batch.</summary>
    public int? TotalCharges { get; set; }

    /// <summary>Initiate and Fetch only: number of charges not yet processed.</summary>
    public int? PendingCharges { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}
