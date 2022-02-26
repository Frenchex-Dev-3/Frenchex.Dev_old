using Frenchex.Dev.Dotnet.Cli.Integration.Lib.Domain;
using Frenchex.Dev.Dotnet.Cli.Lib.Domain;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Frenchex.Dev.Cli;

/// <summary>
///     Implements BasicHostedService for this Program
/// </summary>
public class Host : BasicHostedService
{
    /// <summary>
    ///     Constructor for this Program Host
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="hostApplicationLifetime"></param>
    /// <param name="entryPointInfo"></param>
    /// <param name="integrations"></param>
    public Host(
        ILogger<AbstractHostedService> logger,
        IHostApplicationLifetime hostApplicationLifetime,
        IEntrypointInfo entryPointInfo,
        IEnumerable<IIntegration> integrations
    ) : base("Frenchex.Dev.Cli", logger, hostApplicationLifetime, entryPointInfo, integrations)
    {
    }
}