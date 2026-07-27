namespace PayStack.Net.Models.Responses;

/// <summary>Response data for <c>GET /subscription/:code/manage/link/</c>.</summary>
public sealed class SubscriptionLinkData
{
    /// <summary>A hosted link the customer can visit to update their subscription's card/authorization.</summary>
    public string Link { get; set; } = string.Empty;
}
