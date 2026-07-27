namespace PayStack.Net.Models.Requests;

/// <summary>Request body for <c>DELETE /virtual_terminal/:code/split_code</c>.</summary>
public sealed class RemoveVirtualTerminalSplitCodeRequest
{
    /// <summary>Split code to remove from the virtual terminal. Required.</summary>
    public required string SplitCode { get; set; }
}
