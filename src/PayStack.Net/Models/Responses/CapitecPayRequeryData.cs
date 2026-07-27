namespace PayStack.Net.Models.Responses;

/// <summary>The result of requerying a Capitec Pay transaction.</summary>
public sealed class CapitecPayRequeryData
{
    /// <summary>The transaction's current status, e.g. "success", "pending", "failed". See <see cref="Common.PayStackCapitecPayStatus"/>.</summary>
    public string Status { get; set; } = string.Empty;
}
