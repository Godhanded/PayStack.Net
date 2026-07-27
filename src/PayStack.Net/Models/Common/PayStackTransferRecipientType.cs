namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known values for <see cref="Responses.TransferRecipientData.Type"/> / the <c>type</c> field
/// when creating a transfer recipient. See <see cref="PayStackChannel"/> for the rationale on using
/// string constants instead of an enum.
/// </summary>
public static class PayStackTransferRecipientType
{
    /// <summary>Nigerian bank account.</summary>
    public const string Nuban = "nuban";

    /// <summary>Ghanaian bank account.</summary>
    public const string Ghipss = "ghipss";

    public const string MobileMoney = "mobile_money";

    /// <summary>South African bank account.</summary>
    public const string Basa = "basa";

    /// <summary>Recipient identified by a previously stored authorization code rather than bank details.</summary>
    public const string Authorization = "authorization";
}
