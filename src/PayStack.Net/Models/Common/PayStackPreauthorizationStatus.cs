namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known values for a preauthorization reservation's <c>status</c>.
/// See <see cref="PayStackChannel"/> for the rationale on string constants over an enum.
/// </summary>
public static class PayStackPreauthorizationStatus
{
    public const string Authorized = "authorized";
    public const string Captured = "captured";
    public const string Released = "released";
    public const string Ongoing = "ongoing";
    public const string Failed = "failed";
    public const string Abandoned = "abandoned";
}
