using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;

public interface IRamMbOptionBuilder
{
    Option<int> Build();
}

public class RamMbOptionBuilder : IRamMbOptionBuilder
{
    public Option<int> Build()
    {
        return new Option<int>(new[] {"--ram", "-r"}, () => 128, "RAM in MB");
    }
}