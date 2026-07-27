namespace PayStack.Net.Configuration;

/// <summary>
/// Identifies which Paystack environment a secret key belongs to. Paystack determines the
/// environment from the key prefix (<c>sk_test_</c> / <c>sk_live_</c>) rather than the URL,
/// since both environments share the same base URL (<c>https://api.paystack.co</c>).
/// </summary>
public enum PayStackEnvironment
{
    /// <summary>Sandbox environment. Used automatically when the secret key starts with <c>sk_test_</c>.</summary>
    Sandbox,

    /// <summary>Live/production environment. Used automatically when the secret key starts with <c>sk_live_</c>.</summary>
    Live
}
