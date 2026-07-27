namespace PayStack.Net.Models.Responses;

/// <summary>The result of validating a bank account against a provided identity document.</summary>
public sealed class ValidatedAccountData
{
    public bool AccountAcceptsDebits { get; set; }

    public bool AccountAcceptsCredits { get; set; }

    public bool AccountHolderMatch { get; set; }

    public bool AccountOpenForMoreThanThreeMonths { get; set; }

    public bool AccountOpen { get; set; }

    public bool Verified { get; set; }

    public string? VerificationMessage { get; set; }
}
