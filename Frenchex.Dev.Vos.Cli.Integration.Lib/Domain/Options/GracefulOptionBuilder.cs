using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;

public interface IGracefulOptionBuilder
{
    Option<bool> Build();
}

public class GracefulOptionBuilder : IGracefulOptionBuilder
{
    public Option<bool> Build() => new(new[] { "--graceful", "-g" }, "Graceful");
}