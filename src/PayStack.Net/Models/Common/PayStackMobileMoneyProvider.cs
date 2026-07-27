namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known values for <see cref="Requests.ChargeMobileMoneyDetails.Provider"/>.
/// See <see cref="PayStackChannel"/> for the rationale on string constants over an enum.
/// </summary>
public static class PayStackMobileMoneyProvider
{
    public const string Mtn = "mtn";
    public const string Atl = "atl";
    public const string Vod = "vod";
    public const string Mpesa = "mpesa";
    public const string Orange = "orange";
    public const string Wave = "wave";
    public const string MpesaOffline = "mpesa_offline";
    public const string Mptill = "mptill";
}
