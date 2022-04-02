using Frenchex.Dev.Dotnet.Process.Lib.Domain.Process;

namespace Frenchex.Dev.Dotnet.Docker.Compose.Lib.Domain.Commands.Ps;

public class PsCommandResponse
{
    public ProcessExecutionResult Result { get; }
    
    public PsCommandResponse(ProcessExecutionResult result)
    {
        Result = result;
    }
}