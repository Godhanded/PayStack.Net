using System.Text.Json;
using FluentAssertions;
using PayStack.Net.Models.Responses;
using Xunit;

namespace PayStack.Net.Tests;

public class PayStackJsonOptionsTests
{
    [Fact]
    public void Default_deserializes_paystacks_actual_snake_case_field_names()
    {
        // Real shape of POST /transaction/initialize's "data" object — Paystack's JSON is
        // snake_case, not camelCase, for every multi-word field.
        const string json = """
            {
                "authorization_url": "https://checkout.paystack.com/z22py6yuyh37g9q",
                "access_code": "z22py6yuyh37g9q",
                "reference": "debug-ref-001"
            }
            """;

        var data = JsonSerializer.Deserialize<InitializeTransactionData>(json, PayStackJsonOptions.Default);

        data.Should().NotBeNull();
        data!.AuthorizationUrl.Should().Be("https://checkout.paystack.com/z22py6yuyh37g9q");
        data.AccessCode.Should().Be("z22py6yuyh37g9q");
        data.Reference.Should().Be("debug-ref-001");
    }

    [Fact]
    public void Default_deserializes_multi_word_transaction_fields()
    {
        const string json = """
            {
                "id": 1,
                "status": "success",
                "reference": "ref-1",
                "amount": 500000,
                "receipt_number": "RCPT-1",
                "gateway_response": "Successful",
                "paid_at": "2026-01-01T00:00:00.000Z",
                "authorization": { "authorization_code": "AUTH_xxx" },
                "customer": { "id": 2, "email": "a@b.com", "customer_code": "CUS_xxx" }
            }
            """;

        var data = JsonSerializer.Deserialize<TransactionData>(json, PayStackJsonOptions.Default);

        data.Should().NotBeNull();
        data!.ReceiptNumber.Should().Be("RCPT-1");
        data.GatewayResponse.Should().Be("Successful");
        data.Authorization!.AuthorizationCode.Should().Be("AUTH_xxx");
        data.Customer!.CustomerCode.Should().Be("CUS_xxx");
    }
}
