using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Options;
using PayStack.Net.Configuration;
using PayStack.Net.Http;
using PayStack.Net.Resources;
using Polly;
using Refit;

namespace PayStack.Net;

/// <summary>
/// Dependency injection entry point for the Paystack SDK.
/// </summary>
public static class ServiceCollectionExtensions
{
    private const string HttpClientName = "PayStack";
    private static readonly Uri DefaultBaseUrl = new("https://api.paystack.co");

    /// <summary>
    /// Registers the Paystack SDK: options, a resilient authenticated <see cref="HttpClient"/> pipeline,
    /// every typed Refit resource client, and the aggregate <see cref="IPayStackClient"/> facade.
    /// </summary>
    /// <param name="services">The service collection to register into.</param>
    /// <param name="configure">
    /// Configures <see cref="PayStackOptions"/>, e.g. <c>options.SecretKey = "sk_test_..."</c>.
    /// At minimum, <see cref="PayStackOptions.SecretKey"/> must be set.
    /// </param>
    /// <example>
    /// <code>
    /// builder.Services.AddPayStack(options =>
    /// {
    ///     options.SecretKey = builder.Configuration["PayStack:SecretKey"]!;
    /// });
    /// </code>
    /// </example>
    public static IServiceCollection AddPayStack(this IServiceCollection services, Action<PayStackOptions> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);

        services.AddOptions<PayStackOptions>()
            .Configure(configure)
            .Validate(o => !string.IsNullOrWhiteSpace(o.SecretKey), "PayStack:SecretKey must be configured.")
            .ValidateOnStart();

        return services.AddPayStackCore();
    }

    /// <summary>
    /// Registers the Paystack SDK, binding <see cref="PayStackOptions"/> from the supplied
    /// <see cref="Microsoft.Extensions.Configuration.IConfiguration"/> section (defaults to the <c>"PayStack"</c> section).
    /// </summary>
    /// <example>
    /// <code>
    /// builder.Services.AddPayStack(builder.Configuration);
    /// // appsettings.json: { "PayStack": { "SecretKey": "sk_test_..." } }
    /// </code>
    /// </example>
    public static IServiceCollection AddPayStack(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration, string sectionName = PayStackOptions.SectionName)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddOptions<PayStackOptions>()
            .Bind(configuration.GetSection(sectionName))
            .Validate(o => !string.IsNullOrWhiteSpace(o.SecretKey), "PayStack:SecretKey must be configured.")
            .ValidateOnStart();

        return services.AddPayStackCore();
    }

    private static IServiceCollection AddPayStackCore(this IServiceCollection services)
    {
        services.AddTransient<PayStackAuthHandler>();
        services.AddTransient<IdempotencyKeyHandler>();
        services.AddTransient<PayStackLoggingHandler>();
        services.AddTransient<PayStackExceptionTranslationHandler>();

        var httpClientBuilder = services.AddHttpClient(HttpClientName, (sp, client) =>
            {
                var options = sp.GetRequiredService<IOptionsMonitor<PayStackOptions>>().CurrentValue;
                client.BaseAddress = options.BaseUrlOverride ?? DefaultBaseUrl;
                client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            })
            // Order matters: handlers run outside-in in the order added below is reversed by HttpClientFactory,
            // so this list executes, from the caller's perspective, translation -> logging -> resilience -> idempotency -> auth -> network.
            .AddHttpMessageHandler<PayStackAuthHandler>()
            .AddHttpMessageHandler<IdempotencyKeyHandler>();

        httpClientBuilder.AddResilienceHandler("PayStackResilience", (builder, context) =>
        {
            var options = context.ServiceProvider.GetRequiredService<IOptionsMonitor<PayStackOptions>>().CurrentValue;

            builder.AddTimeout(options.AttemptTimeout);

            if (options.MaxRetryAttempts > 0)
            {
                builder.AddRetry(new HttpRetryStrategyOptions
                {
                    MaxRetryAttempts = options.MaxRetryAttempts,
                    BackoffType = DelayBackoffType.Exponential,
                    UseJitter = true,
                    Delay = TimeSpan.FromMilliseconds(500),
                    ShouldHandle = static args => ValueTask.FromResult(
                        args.Outcome.Exception is not null
                        || args.Outcome.Result is { } response
                           && (response.StatusCode is HttpStatusCode.RequestTimeout or HttpStatusCode.TooManyRequests
                               || (int)response.StatusCode >= 500)),
                });
            }

            builder.AddCircuitBreaker(new HttpCircuitBreakerStrategyOptions
            {
                FailureRatio = 0.5,
                MinimumThroughput = Math.Max(2, options.CircuitBreakerFailureThreshold / 2),
                SamplingDuration = TimeSpan.FromSeconds(30),
                BreakDuration = options.CircuitBreakerBreakDuration,
                ShouldHandle = static args => ValueTask.FromResult(
                    args.Outcome.Exception is not null
                    || args.Outcome.Result is { } response && (int)response.StatusCode >= 500),
            });

            builder.AddTimeout(options.TotalTimeout);
        });

        httpClientBuilder
            .AddHttpMessageHandler<PayStackLoggingHandler>()
            .AddHttpMessageHandler<PayStackExceptionTranslationHandler>();

        var refitSettings = PayStackRefitSettingsFactory.Create();

        services.AddPayStackRefitClient<ITransactionsClient>(refitSettings);
        services.AddPayStackRefitClient<ITransactionSplitsClient>(refitSettings);
        services.AddPayStackRefitClient<IChargeClient>(refitSettings);
        services.AddPayStackRefitClient<IPreauthorizationClient>(refitSettings);
        services.AddPayStackRefitClient<ICustomersClient>(refitSettings);
        services.AddPayStackRefitClient<IDirectDebitClient>(refitSettings);
        services.AddPayStackRefitClient<IDedicatedVirtualAccountsClient>(refitSettings);
        services.AddPayStackRefitClient<IApplePayClient>(refitSettings);
        services.AddPayStackRefitClient<ICapitecPayClient>(refitSettings);
        services.AddPayStackRefitClient<ISubaccountsClient>(refitSettings);
        services.AddPayStackRefitClient<IVerificationClient>(refitSettings);
        services.AddPayStackRefitClient<IMiscellaneousClient>(refitSettings);
        services.AddPayStackRefitClient<IPlansClient>(refitSettings);
        services.AddPayStackRefitClient<ISubscriptionsClient>(refitSettings);
        services.AddPayStackRefitClient<IProductsClient>(refitSettings);
        services.AddPayStackRefitClient<IStorefrontsClient>(refitSettings);
        services.AddPayStackRefitClient<IOrdersClient>(refitSettings);
        services.AddPayStackRefitClient<IPaymentPagesClient>(refitSettings);
        services.AddPayStackRefitClient<IPaymentRequestsClient>(refitSettings);
        services.AddPayStackRefitClient<ISettlementsClient>(refitSettings);
        services.AddPayStackRefitClient<ITransferRecipientsClient>(refitSettings);
        services.AddPayStackRefitClient<ITransfersClient>(refitSettings);
        services.AddPayStackRefitClient<ITransfersControlClient>(refitSettings);
        services.AddPayStackRefitClient<IBulkChargesClient>(refitSettings);
        services.AddPayStackRefitClient<IIntegrationClient>(refitSettings);
        services.AddPayStackRefitClient<IDisputesClient>(refitSettings);
        services.AddPayStackRefitClient<IRefundsClient>(refitSettings);
        services.AddPayStackRefitClient<ITerminalClient>(refitSettings);
        services.AddPayStackRefitClient<IVirtualTerminalClient>(refitSettings);

        services.AddSingleton<IPayStackClient, PayStackClient>();
        services.AddSingleton<Webhooks.IPayStackWebhookParser, Webhooks.PayStackWebhookParser>();

        return services;
    }

    /// <summary>
    /// Registers a single Refit resource interface bound to the shared, resilient, authenticated
    /// "PayStack" <see cref="HttpClient"/>. Internal helper reused across all resource registrations
    /// so every client shares identical auth, retry, circuit breaker, idempotency, and logging behavior.
    /// </summary>
    private static IServiceCollection AddPayStackRefitClient<TClient>(this IServiceCollection services, RefitSettings refitSettings)
        where TClient : class
    {
        services.AddTransient(sp =>
        {
            var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient(HttpClientName);
            return RestService.For<TClient>(httpClient, refitSettings);
        });

        return services;
    }
}
