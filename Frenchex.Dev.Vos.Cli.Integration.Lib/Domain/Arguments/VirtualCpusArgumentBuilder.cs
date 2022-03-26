using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Arguments;

public interface IVirtualCpusArgumentBuilder
{
    Argument<int> Build();
}

public class VirtualCpusArgumentBuilder : IVirtualCpusArgumentBuilder
{
    public Argument<int> Build()
    {
        return new("vcpus", "Virtual CPUs");
    }
}