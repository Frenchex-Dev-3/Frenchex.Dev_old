using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;

public interface IPrimaryOptionBuilder
{
    Option<bool> Build();
}

public class PrimaryOptionBuilder : IPrimaryOptionBuilder
{
    public Option<bool> Build()
    {
        return new(new[] {"--primary"}, "Primary");
    }
}