using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Destroy;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Halt;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Init;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Ssh;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.SshConfig;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Status;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Up;
using Microsoft.Extensions.DependencyInjection;

namespace Frenchex.Dev.Vagrant.Lib.DependencyInjection;

public class ServicesConfiguration
{
    public static IServiceCollection ConfigureServices(IServiceCollection services)
    {
        Dotnet.Filesystem.Lib.DependencyInjection.ServicesConfiguration
            .ConfigureServices(services)
            ;

        Dotnet.Process.Lib.DependencyInjection.ServicesConfiguration
            .ConfigureServices(services)
            ;

        services
            .AddTransient<IBaseCommandRequestBuilderFactory, BaseCommandRequestBuilderFactory>()
            .AddSingleton<IDestroyCommand, DestroyCommand>()
            .AddTransient<IDestroyCommandRequestBuilder, DestroyCommandRequestBuilder>()
            .AddSingleton<IDestroyCommandRequestBuilderFactory, DestroyCommandRequestBuilderFactory>()
            .AddSingleton<IDestroyCommandResponseBuilder, DestroyCommandResponseBuilder>()
            .AddSingleton<IDestroyCommandResponseBuilderFactory, DestroyCommandResponseBuilderFactory>()
            .AddSingleton<IHaltCommand, HaltCommand>()
            .AddSingleton<IHaltCommandRequestBuilder, HaltCommandRequestBuilder>()
            .AddSingleton<IHaltCommandRequestBuilderFactory, HaltCommandRequestBuilderFactory>()
            .AddSingleton<IHaltCommandResponseBuilder, HaltCommandResponseBuilder>()
            .AddSingleton<IHaltCommandResponseBuilderFactory, HaltCommandResponseBuilderFactory>()
            .AddSingleton<IInitCommand, InitCommand>()
            .AddSingleton<IInitCommandRequestBuilder, InitCommandRequestBuilder>()
            .AddSingleton<IInitCommandRequestBuilderFactory, InitCommandRequestBuilderFactory>()
            .AddSingleton<IInitCommandResponseBuilder, InitCommandResponseBuilder>()
            .AddSingleton<IInitCommandResponseBuilderFactory, InitCommandResponseBuilderFactory>()
            .AddSingleton<IUpCommand, UpCommand>()
            .AddSingleton<IUpCommandRequestBuilder, UpCommandRequestBuilder>()
            .AddSingleton<IUpCommandRequestBuilderFactory, UpCommandRequestBuilderFactory>()
            .AddSingleton<IUpCommandResponseBuilder, UpCommandResponseBuilder>()
            .AddSingleton<IUpCommandResponseBuilderFactory, UpCommandResponseBuilderFactory>()
            .AddSingleton<ISshConfigCommand, SshConfigCommand>()
            .AddSingleton<ISshConfigCommandRequestBuilder, SshConfigCommandRequestBuilder>()
            .AddSingleton<ISshConfigCommandRequestBuilderFactory, SshConfigCommandRequestBuilderFactory>()
            .AddSingleton<ISshConfigCommandResponseBuilder, SshConfigCommandResponseBuilder>()
            .AddSingleton<ISshConfigCommandResponseBuilderFactory, SshConfigCommandResponseBuilderFactory>()
            .AddSingleton<ISshCommand, SshCommand>()
            .AddSingleton<ISshCommandRequestBuilderFactory, SshCommandRequestBuilderFactory>()
            .AddSingleton<ISshCommandResponseBuilder, SshCommandResponseBuilder>()
            .AddSingleton<ISshCommandResponseBuilderFactory, SshCommandResponseBuilderFactory>()
            .AddSingleton<IStatusCommand, StatusCommand>()
            .AddSingleton<IStatusCommandRequestBuilderFactory, StatusCommandRequestBuilderFactory>()
            .AddSingleton<IStatusCommandResponseBuilder, StatusCommandResponseBuilder>()
            .AddSingleton<IStatusCommandResponseBuilderFactory, StatusCommandResponseBuilderFactory>()
            ;

        return services;
    }
}