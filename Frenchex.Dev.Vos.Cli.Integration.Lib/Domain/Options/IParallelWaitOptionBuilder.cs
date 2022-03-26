using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;

public interface IParallelWaitOptionBuilder
{
    Option<int> Build();
}

public class ParallelWaitOptionBuilder : IParallelWaitOptionBuilder
{
    public Option<int> Build()
    {
        return new Option<int>(new[] {"--parallel-wait", "-a"},
            () => Environment.ProcessorCount, "Parallel Wait");
    }
}