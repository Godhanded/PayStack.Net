using System.Text.Json;
using FluentAssertions;
using PayStack.Net.Webhooks;
using Xunit;

namespace PayStack.Net.Tests.Webhooks;

public class PayStackWebhookEventTests
{
    private sealed class SamplePayload
    {
        public long Id { get; set; }

        public string Status { get; set; } = string.Empty;
    }

    [Fact]
    public void GetData_deserializes_the_data_element_into_the_requested_type()
    {
        var element = JsonDocument.Parse("""{"id":42,"status":"success"}""").RootElement;
        var evt = new PayStackWebhookEvent { Event = "charge.success", Data = element };

        var data = evt.GetData<SamplePayload>();

        data.Id.Should().Be(42);
        data.Status.Should().Be("success");
    }

    [Fact]
    public void TryGetData_returns_false_when_the_shape_does_not_match()
    {
        var element = JsonDocument.Parse("""[1,2,3]""").RootElement;
        var evt = new PayStackWebhookEvent { Event = "subscription.expiring_cards", Data = element };

        var success = evt.TryGetData<SamplePayload>(out var data);

        success.Should().BeFalse();
        data.Should().BeNull();
    }
}
