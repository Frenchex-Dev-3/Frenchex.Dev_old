using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;

public interface INetworkBridgeOptionBuilder
{
    Option<string> Build();
}

internal class NetworkBridgeOptionBuilder : INetworkBridgeOptionBuilder
{
    public Option<string> Build()
    {
        return new Option<string>(new[] {"--network-bridge"}, "Network Bridge");
    }
}