namespace PayStack.Net.Models.Common;

/// <summary>A single step recorded in a transaction's <see cref="TransactionLog"/> history.</summary>
public sealed class TransactionLogEvent
{
    public string? Type { get; set; }

    public string? Message { get; set; }

    /// <summary>Seconds elapsed since the transaction attempt started.</summary>
    public long Time { get; set; }
}

/// <summary>
/// The step-by-step attempt log for a transaction, returned both embedded on transaction objects
/// and standalone from <c>GET /transaction/timeline/:id_or_reference</c>.
/// </summary>
public sealed class TransactionLog
{
    public long StartTime { get; set; }

    public int TimeSpent { get; set; }

    public int Attempts { get; set; }

    public int Errors { get; set; }

    public bool Success { get; set; }

    public bool Mobile { get; set; }

    public List<string>? Input { get; set; }

    public List<TransactionLogEvent>? History { get; set; }
}
