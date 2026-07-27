using FluentAssertions;
using PayStack.Net.Configuration;
using Xunit;

namespace PayStack.Net.Tests.Configuration;

public class PayStackOptionsTests
{
    [Theory]
    [InlineData("sk_live_abc123", PayStackEnvironment.Live)]
    [InlineData("sk_test_abc123", PayStackEnvironment.Sandbox)]
    [InlineData("", PayStackEnvironment.Sandbox)]
    public void ResolveEnvironment_derives_environment_from_secret_key_prefix(string secretKey, PayStackEnvironment expected)
    {
        var options = new PayStackOptions { SecretKey = secretKey };

        options.ResolveEnvironment().Should().Be(expected);
    }
}
