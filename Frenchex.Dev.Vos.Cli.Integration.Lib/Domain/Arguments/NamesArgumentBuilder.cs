using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Arguments;

public interface INamesArgumentBuilder
{
    Argument<string[]> Build();
}

public class NamesArgumentBuilder : INamesArgumentBuilder

{
    public Argument<string[]> Build()
    {
        return new("names", "Names");
    }
}