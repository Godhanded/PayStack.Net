namespace PayStack.Net.Configuration;

/// <summary>
/// Configuration for the Paystack SDK. Bind this from configuration (e.g. <c>appsettings.json</c>,
/// user secrets, or environment variables) or configure it inline when calling
/// <see cref="ServiceCollectionExtensions.AddPayStack(Microsoft.Extensions.DependencyInjection.IServiceCollection, System.Action{PayStackOptions})"/>.
/// </summary>
public sealed class PayStackOptions
{
    /// <summary>
    /// The configuration section name used when binding via <c>IConfiguration.GetSection(SectionName)</c>.
    /// </summary>
    public const string SectionName = "PayStack";

    /// <summary>
    /// Your Paystack secret key (e.g. <c>sk_test_xxx</c> for sandbox or <c>sk_live_xxx</c> for production).
    /// This is sent as a Bearer token on every request and is never logged.
    /// </summary>
    public string SecretKey { get; set; } = string.Empty;

    /// <summary>
    /// Overrides the API base URL. Leave unset to use the default <c>https://api.paystack.co</c>,
    /// which serves both sandbox and live traffic — Paystack distinguishes the two by the
    /// <see cref="SecretKey"/> prefix, not the URL. Set this only for testing against a proxy,
    /// mock server, or region-specific gateway.
    /// </summary>
    public Uri? BaseUrlOverride { get; set; }

    /// <summary>
    /// The webhook signing check uses this secret key (falls back to <see cref="SecretKey"/> when null).
    /// Paystack webhooks are signed with your account's secret key, so this normally does not need
    /// to be set separately — it exists for setups that verify webhooks with a different key than the
    /// one used for outbound API calls (e.g. a webhook-only, least-privilege key).
    /// </summary>
    public string? WebhookSecretKey { get; set; }

    /// <summary>
    /// Maximum number of automatic retry attempts for transient failures (network errors, timeouts,
    /// and HTTP 5xx/429 responses). Defaults to 3. Set to 0 to disable retries.
    /// </summary>
    public int MaxRetryAttempts { get; set; } = 3;

    /// <summary>
    /// Per-attempt request timeout. Defaults to 30 seconds. Applies to each individual HTTP attempt,
    /// not the overall call including retries.
    /// </summary>
    public TimeSpan AttemptTimeout { get; set; } = TimeSpan.FromSeconds(30);

    /// <summary>
    /// Overall timeout across all attempts of a single logical call (including retries). Defaults to 90 seconds.
    /// </summary>
    public TimeSpan TotalTimeout { get; set; } = TimeSpan.FromSeconds(90);

    /// <summary>
    /// Number of consecutive failures within the circuit breaker sampling window before the circuit
    /// opens and short-circuits further calls with a fast <see cref="PayStack.Net.Http.PayStackCircuitOpenException"/>
    /// instead of hitting the network. Defaults to 8. Guards against hammering Paystack (or your own
    /// network) during a sustained outage.
    /// </summary>
    public int CircuitBreakerFailureThreshold { get; set; } = 8;

    /// <summary>
    /// How long the circuit stays open before allowing a trial request through. Defaults to 30 seconds.
    /// </summary>
    public TimeSpan CircuitBreakerBreakDuration { get; set; } = TimeSpan.FromSeconds(30);

    /// <summary>
    /// When true (default), a fresh <c>Idempotency-Key</c> header is automatically generated and attached
    /// to state-changing requests (POST/PUT) on endpoints that support idempotency (transfers, transfer
    /// recipients, refunds, etc.) when the caller does not explicitly supply one. Set false to opt out and
    /// manage idempotency keys yourself.
    /// </summary>
    public bool AutoGenerateIdempotencyKeys { get; set; } = true;

    /// <summary>
    /// Resolves the effective <see cref="PayStackEnvironment"/> from <see cref="SecretKey"/>'s prefix.
    /// </summary>
    public PayStackEnvironment ResolveEnvironment()
    {
        return SecretKey.StartsWith("sk_live_", StringComparison.Ordinal)
            ? PayStackEnvironment.Live
            : PayStackEnvironment.Sandbox;
    }
}
