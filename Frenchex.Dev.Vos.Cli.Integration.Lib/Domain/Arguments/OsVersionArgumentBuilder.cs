using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Arguments;

public interface IOsVersionArgumentBuilder
{
    Argument<string> Build();
}

public class OsVersionArgumentBuilder : IOsVersionArgumentBuilder
{
    public Argument<string> Build()
    {
        return new Argument<string>("os-version", "OS Version");
    }
}