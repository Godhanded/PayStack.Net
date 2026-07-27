namespace PayStack.Net.Models.Responses;

/// <summary>A bank supported by Paystack, as returned by the "list banks" endpoint.</summary>
public sealed class BankData
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Slug { get; set; }

    /// <summary>The bank code, used elsewhere in the API (e.g. account resolution, subaccount creation).</summary>
    public string? Code { get; set; }

    public string? Longcode { get; set; }

    /// <summary>The gateway this bank uses, e.g. "emandate", "digitalbankmandate", when applicable. See <see cref="Common.PayStackBankGateway"/>.</summary>
    public string? Gateway { get; set; }

    public bool? PayWithBank { get; set; }

    public bool Active { get; set; }

    public bool IsDeleted { get; set; }

    /// <summary>The country this bank operates in, e.g. "Nigeria". See <see cref="Common.PayStackBankCountry"/>.</summary>
    public string? Country { get; set; }

    public string? Currency { get; set; }

    /// <summary>The bank's type, e.g. "nuban", "mobile_money", "ghipss".</summary>
    public string? Type { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}
