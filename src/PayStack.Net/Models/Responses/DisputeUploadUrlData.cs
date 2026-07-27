namespace PayStack.Net.Models.Responses;

/// <summary>A pre-signed upload URL, returned by <c>GET /dispute/:id/upload_url</c>.</summary>
public sealed class DisputeUploadUrlData
{
    /// <summary>Pre-signed URL to <c>PUT</c> the evidence file to.</summary>
    public string SignedUrl { get; set; } = string.Empty;

    /// <summary>The filename to reference (e.g. via <c>UpdateAsync</c>'s <c>uploaded_filename</c>) once uploaded.</summary>
    public string FileName { get; set; } = string.Empty;
}
