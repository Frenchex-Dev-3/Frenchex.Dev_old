using Frenchex.Dev.Dotnet.Docker.Compose.Lib.Domain;
using Frenchex.Dev.Dotnet.Docker.Compose.Lib.Domain.Commands.Init;
using Frenchex.Dev.Dotnet.Docker.Compose.Lib.Domain.Commands.Ps;
using Frenchex.Dev.Dotnet.Docker.Compose.Lib.Domain.Declarations;
using Microsoft.Extensions.DependencyInjection;

namespace Frenchex.Dev.Dotnet.Docker.Compose.Lib.DependencyInjection;

public static class ServicesConfigurator
{
    public static void ConfigureServices(IServiceCollection services)
    {
        Process.Lib.DependencyInjection.ServicesConfiguration.ConfigureServices(services);
        Filesystem.Lib.DependencyInjection.ServicesConfiguration.ConfigureServices(services);
        
        AddDockerComposeDeclarationClasses(services);
        AddCommands(services);
    }

    private static void AddCommands(IServiceCollection services)
    {
        services
            .AddScoped<IPsCommand, PsCommand>()
            .AddTransient<PsCommandRequest>()
            .AddTransient<PsCommandResponse>()
            ;

        services
            .AddScoped<IInitCommand, InitCommand>()
            .AddTransient<InitCommandRequest>()
            .AddTransient<InitCommandResponse>()
            ;
    }

    private static void AddDockerComposeDeclarationClasses(IServiceCollection services)
    {
        services
            .AddTransient<DockerComposeDeclaration>()
            .AddTransient<DockerComposeServiceDeclaration>()
            .AddTransient<DockerComposeNetworkDeclaration>()
            .AddTransient<DockerComposeVolumeDeclaration>()
            .AddTransient<DockerComposeServiceBuildDeclaration>()
            .AddTransient<DockerComposeServiceDeployDeclaration>()
            .AddTransient<DockerComposeServicePlacementDeclaration>()
            .AddTransient<DockerComposeServiceRestartPolicyDeclaration>()
            .AddTransient<DockerComposeServiceUpdateConfigDeclaration>()
            ;
    }
}