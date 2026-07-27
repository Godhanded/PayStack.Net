namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>PUT /dispute/:id/resolve</c>.</summary>
public sealed class ResolveDisputeRequest
{
    /// <summary>Resolution outcome. Required. See <see cref="Common.PayStackDisputeResolution"/> for known values.</summary>
    public required string Resolution { get; set; }

    /// <summary>Message explaining the resolution. Required.</summary>
    public required string Message { get; set; }

    /// <summary>Amount to refund, in the currency's subunit. Required.</summary>
    public required long RefundAmount { get; set; }

    /// <summary>Filename of a previously uploaded evidence file. Required.</summary>
    public required string UploadedFilename { get; set; }

    /// <summary>Id of a previously submitted evidence record, when applicable.</summary>
    public long? Evidence { get; set; }
}
