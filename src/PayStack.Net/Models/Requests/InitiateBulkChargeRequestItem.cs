namespace PayStack.Net.Models.Requests;

/// <summary>
/// A single charge within a <c>POST /bulkcharge</c> request. The request body itself is a raw JSON
/// array of these objects, not wrapped in a container object.
/// </summary>
public sealed class InitiateBulkChargeRequestItem
{
    /// <summary>A previously stored, reusable authorization code to charge. Required.</summary>
    public required string Authorization { get; set; }

    /// <summary>Amount to charge, in the currency's subunit. Required.</summary>
    public required long Amount { get; set; }

    /// <summary>Unique transaction reference. Required.</summary>
    public required string Reference { get; set; }
}
