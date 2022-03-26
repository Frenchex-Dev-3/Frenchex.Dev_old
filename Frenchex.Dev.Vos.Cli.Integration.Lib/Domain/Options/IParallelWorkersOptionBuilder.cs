using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;

public interface IParallelWorkersOptionBuilder
{
    Option<int> Build();
}

public class ParallelWorkersOptionBuilder : IParallelWorkersOptionBuilder
{
    public Option<int> Build()
    {
        return new(new[] {"--parallel-workers", "-pw"},
            () => Environment.ProcessorCount, "Parallel Workers");
    }
}