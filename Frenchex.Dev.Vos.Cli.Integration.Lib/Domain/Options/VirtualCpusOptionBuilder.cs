using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;

public interface IVirtualCpusOptionBuilder
{
    Option<int> Build();
}

public class VirtualCpusOptionBuilder : IVirtualCpusOptionBuilder
{
    public Option<int> Build()
    {
        return new Option<int>(new[] {"--vcpus", "-c"}, () => 2, "Virtual CPUs");
    }
}