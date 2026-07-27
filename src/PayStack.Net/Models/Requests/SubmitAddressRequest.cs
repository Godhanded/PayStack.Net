using System.Text.Json.Serialization;

namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /charge/submit_address</c> — continues a pending charge that requested the customer's billing address.</summary>
public sealed class SubmitAddressRequest
{
    /// <summary>The reference of the pending charge. Required.</summary>
    public required string Reference { get; set; }

    public required string Address { get; set; }

    public required string City { get; set; }

    public required string State { get; set; }

    /// <summary>
    /// The postal/zip code. Paystack's parameter table labels this "zipcode" but its own example
    /// payload sends the field as "zip_code" — this is the literal JSON field name that matches
    /// their working example.
    /// </summary>
    [JsonPropertyName("zip_code")]
    public required string ZipCode { get; set; }
}
