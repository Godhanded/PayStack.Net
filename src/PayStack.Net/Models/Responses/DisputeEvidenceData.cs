namespace PayStack.Net.Models.Responses;

/// <summary>Evidence record created by <c>POST /dispute/:id/evidence</c>.</summary>
public sealed class DisputeEvidenceData
{
    public string CustomerEmail { get; set; } = string.Empty;

    public string CustomerName { get; set; } = string.Empty;

    public string CustomerPhone { get; set; } = string.Empty;

    public string ServiceDetails { get; set; } = string.Empty;

    public string? DeliveryAddress { get; set; }

    public DateTimeOffset? DeliveryDate { get; set; }

    /// <summary>The dispute id this evidence belongs to.</summary>
    public long Dispute { get; set; }

    public long Id { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}
