namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /preauthorization/release</c> — releases a reserved preauthorization without charging it.</summary>
public sealed class ReleasePreauthorizationRequest
{
    /// <summary>The reference of the reservation to release. Required.</summary>
    public required string Reference { get; set; }
}
