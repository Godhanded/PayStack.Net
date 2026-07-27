using System.Security.Cryptography;
using System.Text;
using FluentAssertions;
using PayStack.Net.Webhooks;
using Xunit;

namespace PayStack.Net.Tests.Webhooks;

public class PayStackWebhookSignatureVerifierTests
{
    private const string SecretKey = "sk_test_0123456789abcdef";
    private const string Body = """{"event":"charge.success","data":{"id":1,"status":"success"}}""";

    private static string ComputeValidSignature(string body, string secretKey)
    {
        using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(secretKey));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(body));
        return Convert.ToHexString(hash).ToLowerInvariant();
    }

    [Fact]
    public void Verify_returns_true_for_a_correctly_signed_body()
    {
        var signature = ComputeValidSignature(Body, SecretKey);

        var result = PayStackWebhookSignatureVerifier.Verify(Body, signature, SecretKey);

        result.Should().BeTrue();
    }

    [Fact]
    public void Verify_returns_false_when_the_body_was_tampered_with_after_signing()
    {
        var signature = ComputeValidSignature(Body, SecretKey);
        var tamperedBody = Body.Replace("\"success\"", "\"failed\"");

        var result = PayStackWebhookSignatureVerifier.Verify(tamperedBody, signature, SecretKey);

        result.Should().BeFalse();
    }

    [Fact]
    public void Verify_returns_false_for_the_wrong_secret_key()
    {
        var signature = ComputeValidSignature(Body, SecretKey);

        var result = PayStackWebhookSignatureVerifier.Verify(Body, signature, "sk_test_wrong_key");

        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("not-hex-at-all")]
    [InlineData("deadbeef")]
    public void Verify_returns_false_for_missing_or_malformed_signatures(string? signature)
    {
        var result = PayStackWebhookSignatureVerifier.Verify(Body, signature, SecretKey);

        result.Should().BeFalse();
    }

    [Fact]
    public void Verify_returns_false_for_an_empty_secret_key()
    {
        var signature = ComputeValidSignature(Body, SecretKey);

        var result = PayStackWebhookSignatureVerifier.Verify(Body, signature, string.Empty);

        result.Should().BeFalse();
    }
}
