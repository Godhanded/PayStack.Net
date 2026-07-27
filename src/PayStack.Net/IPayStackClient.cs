using PayStack.Net.Resources;

namespace PayStack.Net;

/// <summary>
/// Aggregate entry point exposing every Paystack resource client. Resolve this from DI after calling
/// <c>AddPayStack</c>, or construct one directly via <see cref="PayStackClient.Create(string, Action{Configuration.PayStackOptions}?)"/>
/// for simple, non-DI usage (scripts, console apps).
/// </summary>
public interface IPayStackClient
{
    /// <summary>Initialize, verify, list, fetch, and charge transactions. See <see href="https://paystack.com/docs/api/transaction/"/>.</summary>
    ITransactionsClient Transactions { get; }

    /// <summary>Split a single payment between multiple accounts. See <see href="https://paystack.com/docs/api/split/"/>.</summary>
    ITransactionSplitsClient TransactionSplits { get; }

    /// <summary>Charge a customer directly on a specific channel (bank, USSD, mobile money, QR, EFT, Capitec Pay). See <see href="https://paystack.com/docs/api/charge/"/>.</summary>
    IChargeClient Charge { get; }

    /// <summary>Reserve and later capture funds on a card. See <see href="https://paystack.com/docs/api/preauthorization/"/>.</summary>
    IPreauthorizationClient Preauthorization { get; }

    /// <summary>Create and manage customers. See <see href="https://paystack.com/docs/api/customer/"/>.</summary>
    ICustomersClient Customers { get; }

    /// <summary>Direct debit mandate activation and management. See <see href="https://paystack.com/docs/api/directdebit/"/>.</summary>
    IDirectDebitClient DirectDebit { get; }

    /// <summary>Create and manage dedicated (virtual) bank accounts. See <see href="https://paystack.com/docs/api/dedicated-virtual-account/"/>.</summary>
    IDedicatedVirtualAccountsClient DedicatedVirtualAccounts { get; }

    /// <summary>Register domains for Apple Pay. See <see href="https://paystack.com/docs/api/apple-pay/"/>.</summary>
    IApplePayClient ApplePay { get; }

    /// <summary>Capitec Pay transaction requery. See <see href="https://paystack.com/docs/api/capitec-pay/"/>.</summary>
    ICapitecPayClient CapitecPay { get; }

    /// <summary>Create and manage subaccounts for split settlements. See <see href="https://paystack.com/docs/api/subaccount/"/>.</summary>
    ISubaccountsClient Subaccounts { get; }

    /// <summary>Resolve/validate bank accounts and card BINs. See <see href="https://paystack.com/docs/api/verification/"/>.</summary>
    IVerificationClient Verification { get; }

    /// <summary>Static lookup data: banks, countries, states. See <see href="https://paystack.com/docs/api/miscellaneous/"/>.</summary>
    IMiscellaneousClient Miscellaneous { get; }

    /// <summary>Create and manage billing plans (pricing tiers). See <see href="https://paystack.com/docs/api/plan/"/>.</summary>
    IPlansClient Plans { get; }

    /// <summary>Create and manage recurring subscriptions. See <see href="https://paystack.com/docs/api/subscription/"/>.</summary>
    ISubscriptionsClient Subscriptions { get; }

    /// <summary>Create and manage products for storefronts/payment pages. See <see href="https://paystack.com/docs/api/product/"/>.</summary>
    IProductsClient Products { get; }

    /// <summary>Create and manage storefronts. See <see href="https://paystack.com/docs/api/storefront/"/>.</summary>
    IStorefrontsClient Storefronts { get; }

    /// <summary>Create and manage storefront orders. See <see href="https://paystack.com/docs/api/order/"/>.</summary>
    IOrdersClient Orders { get; }

    /// <summary>Create and manage hosted payment pages. See <see href="https://paystack.com/docs/api/page/"/>.</summary>
    IPaymentPagesClient PaymentPages { get; }

    /// <summary>Create and manage payment requests (invoices). See <see href="https://paystack.com/docs/api/payment-request/"/>.</summary>
    IPaymentRequestsClient PaymentRequests { get; }

    /// <summary>Inspect settlements and their transactions. See <see href="https://paystack.com/docs/api/settlement/"/>.</summary>
    ISettlementsClient Settlements { get; }

    /// <summary>Create and manage transfer recipients (payout destinations). See <see href="https://paystack.com/docs/api/transfer-recipient/"/>.</summary>
    ITransferRecipientsClient TransferRecipients { get; }

    /// <summary>Initiate and manage transfers (payouts). See <see href="https://paystack.com/docs/api/transfer/"/>.</summary>
    ITransfersClient Transfers { get; }

    /// <summary>Manage transfer OTP requirements and inspect balance. See <see href="https://paystack.com/docs/api/transfer-control/"/>.</summary>
    ITransfersControlClient TransfersControl { get; }

    /// <summary>Charge many customers in a single batch. See <see href="https://paystack.com/docs/api/bulk-charge/"/>.</summary>
    IBulkChargesClient BulkCharges { get; }

    /// <summary>Manage account-level integration settings. See <see href="https://paystack.com/docs/api/integration/"/>.</summary>
    IIntegrationClient Integration { get; }

    /// <summary>Manage chargebacks/disputes. See <see href="https://paystack.com/docs/api/dispute/"/>.</summary>
    IDisputesClient Disputes { get; }

    /// <summary>Create and track refunds. See <see href="https://paystack.com/docs/api/refund/"/>.</summary>
    IRefundsClient Refunds { get; }

    /// <summary>Manage POS terminals. See <see href="https://paystack.com/docs/api/terminal/"/>.</summary>
    ITerminalClient Terminal { get; }

    /// <summary>Create and manage virtual terminals. See <see href="https://paystack.com/docs/api/virtual-terminal/"/>.</summary>
    IVirtualTerminalClient VirtualTerminal { get; }
}
