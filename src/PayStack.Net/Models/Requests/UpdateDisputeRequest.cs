namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>PUT /dispute/:id</c>.</summary>
public sealed class UpdateDisputeRequest
{
    /// <summary>Amount to refund, in the currency's subunit. Required.</summary>
    public required long RefundAmount { get; set; }

    /// <summary>Filename of a previously uploaded evidence file to attach.</summary>
    public string? UploadedFilename { get; set; }
}
