namespace PayStack.Net.Models.Common;

/// <summary>
/// Well-known values for <see cref="Webhooks.PayStackWebhookEvent.Event"/>. Modeled as string constants
/// (see <see cref="PayStackChannel"/> for the rationale) so new event types Paystack adds don't require
/// an SDK update to consume — switch on the raw string, using these constants for IntelliSense/typo-safety.
/// </summary>
public static class PayStackWebhookEventType
{
    public const string ChargeSuccess = "charge.success";
    public const string ChargeDisputeCreate = "charge.dispute.create";
    public const string ChargeDisputeRemind = "charge.dispute.remind";
    public const string ChargeDisputeResolve = "charge.dispute.resolve";
    public const string CustomerIdentificationFailed = "customeridentification.failed";
    public const string CustomerIdentificationSuccess = "customeridentification.success";
    public const string DedicatedAccountAssignFailed = "dedicatedaccount.assign.failed";
    public const string DedicatedAccountAssignSuccess = "dedicatedaccount.assign.success";
    public const string InvoiceCreate = "invoice.create";
    public const string InvoicePaymentFailed = "invoice.payment_failed";
    public const string InvoiceUpdate = "invoice.update";
    public const string PaymentRequestPending = "paymentrequest.pending";
    public const string PaymentRequestSuccess = "paymentrequest.success";
    public const string RefundFailed = "refund.failed";
    public const string RefundPending = "refund.pending";
    public const string RefundProcessed = "refund.processed";
    public const string RefundProcessing = "refund.processing";
    public const string SubscriptionCreate = "subscription.create";
    public const string SubscriptionDisable = "subscription.disable";
    public const string SubscriptionExpiringCards = "subscription.expiring_cards";
    public const string SubscriptionNotRenew = "subscription.not_renew";
    public const string TransferFailed = "transfer.failed";
    public const string TransferSuccess = "transfer.success";
    public const string TransferReversed = "transfer.reversed";
}
