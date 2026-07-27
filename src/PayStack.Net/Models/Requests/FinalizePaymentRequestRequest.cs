namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>POST /paymentrequest/finalize/{code}</c>.</summary>
public sealed class FinalizePaymentRequestRequest
{
    /// <summary>Whether to notify the customer. Defaults to <c>true</c>.</summary>
    public bool? SendNotification { get; set; }
}
