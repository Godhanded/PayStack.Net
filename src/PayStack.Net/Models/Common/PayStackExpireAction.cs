namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known values for what happens to a preauthorization reservation that is never captured or
/// released before its expiry. See <see cref="PayStackChannel"/> for the rationale on string
/// constants over an enum.
/// </summary>
public static class PayStackExpireAction
{
    /// <summary>Automatically capture the reserved amount when the reservation expires.</summary>
    public const string Capture = "capture";

    /// <summary>Automatically release the reservation (no charge) when it expires. This is the default.</summary>
    public const string Release = "release";
}
