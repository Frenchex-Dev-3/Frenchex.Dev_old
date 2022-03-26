using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Arguments;

public interface IOsTypeArgumentBuilder
{
    Argument<string> Build();
}

public class OsTypeArgumentBuilder : IOsTypeArgumentBuilder
{
    public Argument<string> Build()
    {
        return new Argument<string>("os-type", "OS Name");
    }
}