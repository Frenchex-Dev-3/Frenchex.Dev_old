using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Arguments;

public interface INameArgumentBuilder
{
    Argument<string> Build();
}

public class NameArgumentBuilder : INameArgumentBuilder

{
    public Argument<string> Build()
    {
        return new("name", "Name");
    }
}