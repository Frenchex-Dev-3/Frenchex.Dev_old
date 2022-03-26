using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Arguments;

public interface IMachineTypeNameArgumentBuilder
{
    Argument<string> Build();
}

public class MachineTypeNameArgumentBuilder : IMachineTypeNameArgumentBuilder
{
    public Argument<string> Build()
    {
        return new("type", "MachineType Name");
    }
}