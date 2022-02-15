using Frenchex.Dev.Dotnet.Process.Lib.Domain.Process;

namespace Frenchex.Dev.Dotnet.Process.Lib.Domain.ProcessBuilder;

public interface IProcessBuilder
{
    IProcess Build(IProcessBuildingParameters parameters);
}

public interface IProcessBuildingParameters
{
    string Command { get; }
    string Arguments { get; }
    string? WorkingDirectory { get; }
    int TimeOutInMiliSeconds { get; }
    bool UseShellExecute { get; }
    bool RedirectStandardInput { get; }
    bool RedirectStandardOutput { get; }
    bool RedirectStandardError { get; }
    bool CreateNoWindow { get; }
}

public class ProcessBuildingParameters : IProcessBuildingParameters
{
    public ProcessBuildingParameters(
        string command,
        string arguments,
        string? workingDirectory,
        int timeoutMs,
        bool useShellExecute,
        bool redirectStandardInput,
        bool redirectStandardOuput,
        bool redirectStandardError,
        bool createNoWindow
    )
    {
        Command = command;
        Arguments = arguments;
        WorkingDirectory = workingDirectory;
        TimeOutInMiliSeconds = timeoutMs;
        UseShellExecute = useShellExecute;
        RedirectStandardInput = redirectStandardInput;
        RedirectStandardOutput = redirectStandardOuput;
        RedirectStandardError = redirectStandardError;
        CreateNoWindow = createNoWindow;
    }

    public string Command { get; set; }
    public string Arguments { get; set; }
    public string? WorkingDirectory { get; set; }
    public int TimeOutInMiliSeconds { get; set; }
    public bool UseShellExecute { get; set; }
    public bool RedirectStandardInput { get; set; }
    public bool RedirectStandardOutput { get; set; }
    public bool RedirectStandardError { get; set; }
    public bool CreateNoWindow { get; set; }
}