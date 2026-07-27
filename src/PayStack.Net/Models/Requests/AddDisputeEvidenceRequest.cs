namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /dispute/:id/evidence</c>.</summary>
public sealed class AddDisputeEvidenceRequest
{
    /// <summary>Customer's email address. Required.</summary>
    public required string CustomerEmail { get; set; }

    /// <summary>Customer's full name. Required.</summary>
    public required string CustomerName { get; set; }

    /// <summary>Customer's phone number. Required.</summary>
    public required string CustomerPhone { get; set; }

    /// <summary>Description of the service/product provided. Required.</summary>
    public required string ServiceDetails { get; set; }

    /// <summary>Delivery address, when relevant to the dispute.</summary>
    public string? DeliveryAddress { get; set; }

    /// <summary>Delivery date (YYYY-MM-DD), when relevant to the dispute.</summary>
    public DateTimeOffset? DeliveryDate { get; set; }
}
