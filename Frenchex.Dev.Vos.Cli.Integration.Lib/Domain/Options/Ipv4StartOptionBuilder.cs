using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;

public interface IIpv4StartOptionBuilder
{
    Option<int> Build();
}

public class Ipv4StartOptionBuilder : IIpv4StartOptionBuilder
{
    public Option<int> Build() => new(new[] { "--ipv4-start" }, () => 2, "IPv4 start");
}