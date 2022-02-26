using Microsoft.Extensions.Hosting;

namespace Frenchex.Dev.Dotnet.Cli.Lib.Domain;

public class Program : IProgram
{
    private readonly IHost _host;

    public Program(
        IHost host
    )
    {
        _host = host;
    }

    public async Task RunAsync()
    {
        await _host.StartAsync();
        await _host.WaitForShutdownAsync();
    }
}