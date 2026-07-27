namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /preauthorization/capture</c> — charges a previously reserved preauthorization.</summary>
public sealed class CapturePreauthorizationRequest
{
    /// <summary>The reference from the initialize or reserve step. Required.</summary>
    public required string Reference { get; set; }

    /// <summary>ISO 4217 currency code. Only "ZAR" is supported. Required.</summary>
    public required string Currency { get; set; }

    /// <summary>Amount to capture, in the currency's subunit. Required.</summary>
    public required string Amount { get; set; }
}
