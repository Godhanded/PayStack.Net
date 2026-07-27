namespace PayStack.Net.Models.Responses;

/// <summary>A single balance movement entry, returned by <c>GET /balance/ledger</c>.</summary>
public sealed class BalanceLedgerEntryData
{
    public long? Integration { get; set; }

    public string? Domain { get; set; }

    /// <summary>Resulting balance after this entry, in the currency's subunit.</summary>
    public long Balance { get; set; }

    public string Currency { get; set; } = string.Empty;

    /// <summary>Change in balance caused by this entry, in the currency's subunit.</summary>
    public long Difference { get; set; }

    public string? Reason { get; set; }

    /// <summary>The type of record that caused this movement, e.g. "Transfer", "Transaction", "Refund".</summary>
    public string? ModelResponsible { get; set; }

    /// <summary>The numeric id of the record referenced by <see cref="ModelResponsible"/>.</summary>
    public long? ModelRow { get; set; }

    public long Id { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}
