namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>PUT /split/:id</c>.</summary>
public sealed class UpdateSplitRequest
{
    /// <summary>Name of the split. Required.</summary>
    public required string Name { get; set; }

    /// <summary>Whether the split is active. Required.</summary>
    public required bool Active { get; set; }

    /// <summary>Who bears the Paystack fee. Use <see cref="Common.PayStackBearerType"/> constants.</summary>
    public string? BearerType { get; set; }

    /// <summary>The subaccount that bears the fee, when <see cref="BearerType"/> is "subaccount".</summary>
    public string? BearerSubaccount { get; set; }
}
