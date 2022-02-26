using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Arguments;

public interface IRamMbArgumentBuilder
{
    Argument<int> Build();
}

public class RamMbArgumentBuilder : IRamMbArgumentBuilder
{
    public Argument<int> Build() => new("ram-mb", "RAM in MB");
}