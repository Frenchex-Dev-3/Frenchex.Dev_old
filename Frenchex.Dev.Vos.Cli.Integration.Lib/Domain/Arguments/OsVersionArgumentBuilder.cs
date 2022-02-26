using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Arguments;

public interface IOsVersionArgumentBuilder
{
    Argument<string> Build();
}

public class OsVersionArgumentBuilder : IOsVersionArgumentBuilder
{
    public Argument<string> Build() => new("os-version", "OS Version");
}