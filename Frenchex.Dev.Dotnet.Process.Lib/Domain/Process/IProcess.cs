namespace Frenchex.Dev.Dotnet.Process.Lib.Domain.Process;

public interface IProcess : IDisposable
{
    public System.Diagnostics.Process WrappedProcess { get; }
    ProcessExecutionResult Start();
    void Stop();
    Task<ProcessExecutionResult> WaitForExitOrTimeOut();
    bool TryKillProcess();
}

public class ProcessExecutionResult
{
    public ProcessExecutionResult(System.Diagnostics.Process process)
    {
        Process = process;
    }

    public bool? Completed { get; set; }
    public int? ExitCode { get; set; }
    public int? TimeOutMiliseconds { get; set; }
    public Exception? Exception { get; set; }
    public TaskCompletionSource<bool>? OutputCloseEvent { get; } = new();
    public TaskCompletionSource<bool>? ErrorCloseEvent { get; } = new();
    public TaskCompletionSource<bool>? ExitedCloseEvent { get; } = new();
    public Task? WaitForExitOrTimeOut { get; set; }
    public Task? WaitForCompleteExit { get; set; }
    public Task<Task>? WaitForAny { get; set; }
    public System.Diagnostics.Process Process { get; }
    public MemoryStream? OutputStream { get; set; }
}