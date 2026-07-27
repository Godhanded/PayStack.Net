using Microsoft.Extensions.DependencyInjection;
using PayStack.Net.Configuration;
using PayStack.Net.Resources;

namespace PayStack.Net;

/// <inheritdoc cref="IPayStackClient"/>
internal sealed class PayStackClient : IPayStackClient
{
    public PayStackClient(
        ITransactionsClient transactions,
        ITransactionSplitsClient transactionSplits,
        IChargeClient charge,
        IPreauthorizationClient preauthorization,
        ICustomersClient customers,
        IDirectDebitClient directDebit,
        IDedicatedVirtualAccountsClient dedicatedVirtualAccounts,
        IApplePayClient applePay,
        ICapitecPayClient capitecPay,
        ISubaccountsClient subaccounts,
        IVerificationClient verification,
        IMiscellaneousClient miscellaneous,
        IPlansClient plans,
        ISubscriptionsClient subscriptions,
        IProductsClient products,
        IStorefrontsClient storefronts,
        IOrdersClient orders,
        IPaymentPagesClient paymentPages,
        IPaymentRequestsClient paymentRequests,
        ISettlementsClient settlements,
        ITransferRecipientsClient transferRecipients,
        ITransfersClient transfers,
        ITransfersControlClient transfersControl,
        IBulkChargesClient bulkCharges,
        IIntegrationClient integration,
        IDisputesClient disputes,
        IRefundsClient refunds,
        ITerminalClient terminal,
        IVirtualTerminalClient virtualTerminal)
    {
        Transactions = transactions;
        TransactionSplits = transactionSplits;
        Charge = charge;
        Preauthorization = preauthorization;
        Customers = customers;
        DirectDebit = directDebit;
        DedicatedVirtualAccounts = dedicatedVirtualAccounts;
        ApplePay = applePay;
        CapitecPay = capitecPay;
        Subaccounts = subaccounts;
        Verification = verification;
        Miscellaneous = miscellaneous;
        Plans = plans;
        Subscriptions = subscriptions;
        Products = products;
        Storefronts = storefronts;
        Orders = orders;
        PaymentPages = paymentPages;
        PaymentRequests = paymentRequests;
        Settlements = settlements;
        TransferRecipients = transferRecipients;
        Transfers = transfers;
        TransfersControl = transfersControl;
        BulkCharges = bulkCharges;
        Integration = integration;
        Disputes = disputes;
        Refunds = refunds;
        Terminal = terminal;
        VirtualTerminal = virtualTerminal;
    }

    public ITransactionsClient Transactions { get; }
    public ITransactionSplitsClient TransactionSplits { get; }
    public IChargeClient Charge { get; }
    public IPreauthorizationClient Preauthorization { get; }
    public ICustomersClient Customers { get; }
    public IDirectDebitClient DirectDebit { get; }
    public IDedicatedVirtualAccountsClient DedicatedVirtualAccounts { get; }
    public IApplePayClient ApplePay { get; }
    public ICapitecPayClient CapitecPay { get; }
    public ISubaccountsClient Subaccounts { get; }
    public IVerificationClient Verification { get; }
    public IMiscellaneousClient Miscellaneous { get; }
    public IPlansClient Plans { get; }
    public ISubscriptionsClient Subscriptions { get; }
    public IProductsClient Products { get; }
    public IStorefrontsClient Storefronts { get; }
    public IOrdersClient Orders { get; }
    public IPaymentPagesClient PaymentPages { get; }
    public IPaymentRequestsClient PaymentRequests { get; }
    public ISettlementsClient Settlements { get; }
    public ITransferRecipientsClient TransferRecipients { get; }
    public ITransfersClient Transfers { get; }
    public ITransfersControlClient TransfersControl { get; }
    public IBulkChargesClient BulkCharges { get; }
    public IIntegrationClient Integration { get; }
    public IDisputesClient Disputes { get; }
    public IRefundsClient Refunds { get; }
    public ITerminalClient Terminal { get; }
    public IVirtualTerminalClient VirtualTerminal { get; }

    /// <summary>
    /// Builds a standalone <see cref="IPayStackClient"/> without requiring an ASP.NET Core / generic
    /// host dependency injection container — suitable for console apps, scripts, and Azure Functions.
    /// Internally spins up a minimal <see cref="ServiceProvider"/> that owns the HTTP pipeline; dispose
    /// the returned client (or the process) when done to release it.
    /// </summary>
    /// <param name="secretKey">Your Paystack secret key (<c>sk_test_...</c> or <c>sk_live_...</c>).</param>
    /// <param name="configure">Optional additional configuration (timeouts, retries, base URL override, etc.).</param>
    /// <example>
    /// <code>
    /// using var client = PayStackClient.Create("sk_test_xxx");
    /// var response = await client.Client.Transactions.InitializeAsync(new InitializeTransactionRequest
    /// {
    ///     Amount = "50000",
    ///     Email = "customer@example.com"
    /// });
    /// </code>
    /// </example>
    public static DisposablePayStackClient Create(string secretKey, Action<PayStackOptions>? configure = null)
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddPayStack(options =>
        {
            options.SecretKey = secretKey;
            configure?.Invoke(options);
        });

        var provider = services.BuildServiceProvider();
        return new DisposablePayStackClient(provider);
    }
}

/// <summary>
/// An <see cref="IPayStackClient"/> paired with the standalone service provider that backs it
/// (created via <see cref="PayStackClient.Create"/>). Dispose to release the underlying HTTP pipeline.
/// </summary>
public sealed class DisposablePayStackClient : IPayStackClient, IDisposable
{
    private readonly ServiceProvider _provider;
    private readonly IPayStackClient _inner;

    internal DisposablePayStackClient(ServiceProvider provider)
    {
        _provider = provider;
        _inner = provider.GetRequiredService<IPayStackClient>();
    }

    /// <summary>The underlying webhook parser, for standalone (non-DI-host) webhook handling.</summary>
    public Webhooks.IPayStackWebhookParser WebhookParser => _provider.GetRequiredService<Webhooks.IPayStackWebhookParser>();

    public ITransactionsClient Transactions => _inner.Transactions;
    public ITransactionSplitsClient TransactionSplits => _inner.TransactionSplits;
    public IChargeClient Charge => _inner.Charge;
    public IPreauthorizationClient Preauthorization => _inner.Preauthorization;
    public ICustomersClient Customers => _inner.Customers;
    public IDirectDebitClient DirectDebit => _inner.DirectDebit;
    public IDedicatedVirtualAccountsClient DedicatedVirtualAccounts => _inner.DedicatedVirtualAccounts;
    public IApplePayClient ApplePay => _inner.ApplePay;
    public ICapitecPayClient CapitecPay => _inner.CapitecPay;
    public ISubaccountsClient Subaccounts => _inner.Subaccounts;
    public IVerificationClient Verification => _inner.Verification;
    public IMiscellaneousClient Miscellaneous => _inner.Miscellaneous;
    public IPlansClient Plans => _inner.Plans;
    public ISubscriptionsClient Subscriptions => _inner.Subscriptions;
    public IProductsClient Products => _inner.Products;
    public IStorefrontsClient Storefronts => _inner.Storefronts;
    public IOrdersClient Orders => _inner.Orders;
    public IPaymentPagesClient PaymentPages => _inner.PaymentPages;
    public IPaymentRequestsClient PaymentRequests => _inner.PaymentRequests;
    public ISettlementsClient Settlements => _inner.Settlements;
    public ITransferRecipientsClient TransferRecipients => _inner.TransferRecipients;
    public ITransfersClient Transfers => _inner.Transfers;
    public ITransfersControlClient TransfersControl => _inner.TransfersControl;
    public IBulkChargesClient BulkCharges => _inner.BulkCharges;
    public IIntegrationClient Integration => _inner.Integration;
    public IDisputesClient Disputes => _inner.Disputes;
    public IRefundsClient Refunds => _inner.Refunds;
    public ITerminalClient Terminal => _inner.Terminal;
    public IVirtualTerminalClient VirtualTerminal => _inner.VirtualTerminal;

    public void Dispose() => _provider.Dispose();
}
