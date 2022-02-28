using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Arguments;

public interface IParallelOptionBuilder
{
    Option<bool> Build();
}

internal class ParallelOptionBuilder : IParallelOptionBuilder
{
    public Option<bool> Build() => new(new[] {"--parallel", "-p"}, () => true, "Parallel");
}