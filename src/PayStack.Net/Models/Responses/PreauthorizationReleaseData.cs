namespace PayStack.Net.Models.Responses;

/// <summary>Response payload from <c>POST /preauthorization/release</c>.</summary>
public sealed class PreauthorizationReleaseData
{
    /// <summary>Always "released" on success. See <see cref="Common.PayStackPreauthorizationStatus"/>.</summary>
    public string Status { get; set; } = string.Empty;

    public string Reference { get; set; } = string.Empty;
}
