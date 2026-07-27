namespace PayStack.Net.Models.Responses;

/// <summary>Per-currency volume total, used in <see cref="TransactionTotalsData"/>.</summary>
public sealed class CurrencyVolume
{
    public string Currency { get; set; } = string.Empty;

    public long Amount { get; set; }
}

/// <summary>Response payload from <c>GET /transaction/totals</c>.</summary>
public sealed class TransactionTotalsData
{
    public long TotalTransactions { get; set; }

    public long TotalVolume { get; set; }

    public List<CurrencyVolume>? TotalVolumeByCurrency { get; set; }

    public long PendingTransfers { get; set; }

    public List<CurrencyVolume>? PendingTransfersByCurrency { get; set; }
}

/// <summary>Response payload from <c>GET /transaction/export</c>.</summary>
public sealed class TransactionExportData
{
    /// <summary>Signed, time-limited URL to download the exported CSV.</summary>
    public string Path { get; set; } = string.Empty;

    public DateTimeOffset ExpiresAt { get; set; }
}
