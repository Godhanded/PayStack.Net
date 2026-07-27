namespace PayStack.Net.Models.Responses;

/// <summary>A signed export download link, returned by <c>GET /dispute/export</c>.</summary>
public sealed class DisputeExportData
{
    /// <summary>Signed, time-limited S3 URL to download the exported disputes CSV from.</summary>
    public string Path { get; set; } = string.Empty;

    public DateTimeOffset ExpiresAt { get; set; }
}
