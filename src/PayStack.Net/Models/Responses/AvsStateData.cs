namespace PayStack.Net.Models.Responses;

/// <summary>A state/province for address verification (AVS), as returned by the "list states" endpoint.</summary>
public sealed class AvsStateData
{
    public string Name { get; set; } = string.Empty;

    public string? Slug { get; set; }

    public string? Abbreviation { get; set; }
}
