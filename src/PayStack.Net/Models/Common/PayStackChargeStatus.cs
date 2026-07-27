namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known values for <see cref="Responses.ChargeData.Status"/>. A direct charge can pause at an
/// intermediate step that requires the caller to collect more information from the customer and call
/// the matching <c>submit_*</c> endpoint on <see cref="Resources.IChargeClient"/> before it resolves
/// to <see cref="Success"/> or <see cref="Failed"/>. See <see cref="PayStackChannel"/> for the
/// rationale on string constants over an enum.
/// </summary>
public static class PayStackChargeStatus
{
    public const string Success = "success";
    public const string Failed = "failed";
    public const string Pending = "pending";

    /// <summary>Call <c>POST /charge/submit_pin</c> to continue.</summary>
    public const string SendPin = "send_pin";

    /// <summary>Call <c>POST /charge/submit_otp</c> to continue.</summary>
    public const string SendOtp = "send_otp";

    /// <summary>Call <c>POST /charge/submit_phone</c> to continue.</summary>
    public const string SendPhone = "send_phone";

    /// <summary>Call <c>POST /charge/submit_birthday</c> to continue.</summary>
    public const string SendBirthday = "send_birthday";

    /// <summary>Call <c>POST /charge/submit_address</c> to continue.</summary>
    public const string SendAddress = "send_address";

    /// <summary>The customer must complete payment through an external channel (e.g. bank app); poll <c>GET /charge/:reference</c>.</summary>
    public const string PayOffline = "pay_offline";
}
