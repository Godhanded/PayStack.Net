using System.Security.Cryptography;
using System.Text;

namespace PayStack.Net.Webhooks;

/// <summary>
/// Verifies the <c>x-paystack-signature</c> header Paystack attaches to every webhook delivery:
/// an HMAC-SHA512 of the raw request body, keyed with your secret key, hex-encoded.
/// Stateless and allocation-light by design — safe to call per-request without DI.
/// </summary>
public static class PayStackWebhookSignatureVerifier
{
    /// <summary>The header Paystack sends the signature in.</summary>
    public const string SignatureHeaderName = "x-paystack-signature";

    /// <summary>
    /// Verifies that <paramref name="signatureHeaderValue"/> is a valid HMAC-SHA512 signature of
    /// <paramref name="rawRequestBody"/> keyed with <paramref name="secretKey"/>. Uses a
    /// constant-time comparison to avoid leaking timing information about the expected signature.
    /// </summary>
    /// <param name="rawRequestBody">
    /// The exact, unmodified request body bytes/string as received — re-serializing a parsed JSON
    /// object before verifying will produce a different signature and always fail. Read the raw body first.
    /// </param>
    /// <param name="signatureHeaderValue">The value of the <c>x-paystack-signature</c> request header.</param>
    /// <param name="secretKey">Your Paystack secret key (the same one used for API calls, unless you use a dedicated webhook key).</param>
    /// <returns><c>true</c> when the signature is valid; <c>false</c> otherwise (including malformed/missing input — never throws for bad input).</returns>
    public static bool Verify(string rawRequestBody, string? signatureHeaderValue, string secretKey)
    {
        if (string.IsNullOrEmpty(signatureHeaderValue) || string.IsNullOrEmpty(rawRequestBody) || string.IsNullOrEmpty(secretKey))
        {
            return false;
        }

        Span<byte> computed = stackalloc byte[64]; // SHA-512 = 64 bytes
        var bodyBytes = Encoding.UTF8.GetBytes(rawRequestBody);
        var keyBytes = Encoding.UTF8.GetBytes(secretKey);

        if (!HMACSHA512.TryHashData(keyBytes, bodyBytes, computed, out var written) || written != computed.Length)
        {
            return false;
        }

        Span<byte> expected = stackalloc byte[64];
        if (!TryParseHex(signatureHeaderValue, expected, out var expectedWritten) || expectedWritten != computed.Length)
        {
            return false;
        }

        return CryptographicOperations.FixedTimeEquals(computed, expected);
    }

    private static bool TryParseHex(string hex, Span<byte> destination, out int written)
    {
        written = 0;
        if (hex.Length % 2 != 0 || hex.Length / 2 > destination.Length)
        {
            return false;
        }

        for (var i = 0; i < hex.Length; i += 2)
        {
            if (!byte.TryParse(hex.AsSpan(i, 2), System.Globalization.NumberStyles.HexNumber, null, out var b))
            {
                return false;
            }

            destination[written++] = b;
        }

        return true;
    }
}
