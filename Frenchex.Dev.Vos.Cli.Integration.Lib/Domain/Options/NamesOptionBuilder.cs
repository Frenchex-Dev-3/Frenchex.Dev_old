using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;

public interface INamesOptionBuilder
{
    Option<string[]> Build();
}

public class NamesOptionBuilder : INamesOptionBuilder
{
    public Option<string[]> Build()
    {
        return new("--name", "Name or ID");
    }
}