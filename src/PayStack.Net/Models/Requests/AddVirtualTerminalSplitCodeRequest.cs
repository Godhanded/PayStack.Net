namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>PUT /virtual_terminal/:code/split_code</c>.</summary>
public sealed class AddVirtualTerminalSplitCodeRequest
{
    /// <summary>Split code of a pre-created transaction split to apply. Required.</summary>
    public required string SplitCode { get; set; }
}
