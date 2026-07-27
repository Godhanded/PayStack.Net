using Refit;

namespace PayStack.Net.Http;

/// <summary>
/// Builds the shared <see cref="RefitSettings"/> used by every resource client: System.Text.Json
/// serialization with Paystack's camelCase conventions, and an <see cref="RefitSettings.ExceptionFactory"/> that
/// always returns <c>null</c> so Refit never throws <see cref="Refit.ApiException"/> for non-success HTTP
/// responses. Every resource method returns <c>Task&lt;ApiResponse&lt;PayStackResponse&lt;T&gt;&gt;&gt;</c>,
/// so callers inspect <c>IsSuccessStatusCode</c> / <c>Error</c> instead of catching exceptions for
/// expected API-level failures (declined transactions, validation errors, not-found, etc.).
/// </summary>
internal static class PayStackRefitSettingsFactory
{
    public static RefitSettings Create() => new()
    {
        ContentSerializer = new SystemTextJsonContentSerializer(PayStackJsonOptions.Default),
        ExceptionFactory = _ => Task.FromResult<Exception?>(null),
    };
}
