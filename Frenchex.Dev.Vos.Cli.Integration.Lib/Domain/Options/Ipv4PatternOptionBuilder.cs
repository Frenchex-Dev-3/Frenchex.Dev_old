using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;

public interface IIpv4PatternOptionBuilder
{
    Option<string> Build();
}

public class Ipv4PatternOptionBuilder : IIpv4PatternOptionBuilder
{
    public Option<string> Build()
    {
        return new(new[] {"--ipv4-pattern"}, () => "10.100.1.#INSTANCE#", "IPv4 pattern");
    }
}