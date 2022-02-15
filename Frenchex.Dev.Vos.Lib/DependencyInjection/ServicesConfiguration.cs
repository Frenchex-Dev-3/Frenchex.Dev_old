using Frenchex.Dev.Vos.Lib.Domain.Actions.Configuration.Load;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Configuration.Save;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Naming;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Networking;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Define.Machine.Add;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Define.MachineType.Add;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Destroy;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Halt;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Init;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Name;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Ssh;
using Frenchex.Dev.Vos.Lib.Domain.Commands.SshConfig;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Status;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Up;
using Frenchex.Dev.Vos.Lib.Domain.Definitions;
using Frenchex.Dev.Vos.Lib.Domain.Resources;
using Microsoft.Extensions.DependencyInjection;

namespace Frenchex.Dev.Vos.Lib.DependencyInjection;

public class ServicesConfiguration
{
    public static IServiceCollection ConfigureServices(IServiceCollection services)
    {
        // dependencies
        Dotnet.Filesystem.Lib.DependencyInjection.ServicesConfiguration
            .ConfigureServices(services)
            ;

        Vagrant.Lib.DependencyInjection.ServicesConfiguration
            .ConfigureServices(services)
            ;

        // resources

        services
            .AddScoped<IVagrantfileResource, VagrantfileResource>()
            ;

        // actions

        services
            .AddScoped<IConfigurationLoadAction, ConfigurationLoadAction>()
            .AddScoped<IConfigurationSaveAction, ConfigurationSaveAction>()
            .AddScoped<IDefaultGatewayGetterAction, DefaultGatewayGetterAction>()
            ;

        // proprosed services
        services

            // Root
            .AddTransient<IBaseRequestBuilderFactory, BaseRequestBuilderFactory>()
            .AddTransient<IBaseRequestBuilder, BaseRequestBuilder>()

            // Bases
            .AddTransient<MachineBaseDefinitionBuilder, MachineBaseDefinitionBuilder>()
            .AddTransient<MachineBaseDefinitionBuilderFactory, MachineBaseDefinitionBuilderFactory>()
            .AddTransient<MachineDefinitionBuilder, MachineDefinitionBuilder>()
            .AddTransient<MachineDefinitionBuilderFactory, MachineDefinitionBuilderFactory>()
            .AddTransient<IMachineTypeDefinitionBuilder, MachineTypeDefinitionBuilder>()
            .AddTransient<IMachineTypeDefinitionBuilderFactory, MachineTypeDefinitionBuilderFactory>()

            // Actions
            .AddScoped<IVexNameToVagrantNameConverter, VexNameToVagrantNameConverter>()

            // Commands
            .AddScoped<INameCommand, NameCommand>()
            .AddScoped<INameCommandRequestBuilder, NameCommandRequestBuilder>()
            .AddScoped<INameCommandRequestBuilderFactory, NameCommandRequestBuilderFactory>()
            .AddScoped<INameCommandResponseBuilder, NameCommandResponseBuilder>()
            .AddScoped<INameCommandResponseBuilderFactory, NameCommandResponseBuilderFactory>()
            .AddScoped<IDefineMachineAddCommand, DefineMachineAddCommand>()
            .AddScoped<IDefineMachineAddCommandRequestBuilder, DefineMachineAddCommandRequestBuilder>()
            .AddScoped<IDefineMachineAddCommandRequestBuilderFactory, DefineMachineAddCommandRequestBuilderFactory>()
            .AddScoped<IDefineMachineAddCommandResponseBuilder, DefineMachineAddCommandResponseBuilder>()
            .AddScoped<IDefineMachineAddCommandResponseBuilderFactory,
                DefineMachineAddCommandResponseBuilderFactory>()
            .AddScoped<IDefineMachineTypeAddCommand, DefineMachineTypeAddCommand>()
            .AddScoped<IDefineMachineTypeAddCommandRequestBuilder, DefineMachineTypeAddCommandRequestBuilder>()
            .AddScoped<IDefineMachineTypeAddCommandRequestBuilderFactory,
                DefineMachineTypeAddCommandRequestBuilderFactory>()
            .AddScoped<IDefineMachineTypeAddCommandResponseBuilder, DefineMachineTypeAddCommandResponseBuilder>()
            .AddScoped<IDefineMachineTypeAddCommandResponseBuilderFactory,
                DefineMachineTypeAddCommandResponseBuilderFactory>()
            .AddScoped<IDestroyCommand, DestroyCommand>()
            .AddScoped<IDestroyCommandRequestBuilder, DestroyCommandRequestBuilder>()
            .AddScoped<IDestroyCommandRequestBuilderFactory, DestroyCommandRequestBuilderFactory>()
            .AddScoped<IDestroyCommandResponseBuilder, DestroyCommandResponseBuilder>()
            .AddScoped<IDestroyCommandResponseBuilderFactory, DestroyCommandResponseBuilderFactory>()
            .AddScoped<IHaltCommand, HaltCommand>()
            .AddScoped<IHaltCommandRequestBuilder, HaltCommandRequestBuilder>()
            .AddScoped<IHaltCommandRequestBuilderFactory, HaltCommandRequestBuilderFactory>()
            .AddScoped<IHaltCommandResponseBuilder, HaltCommandResponseBuilder>()
            .AddScoped<IHaltCommandResponseBuilderFactory, HaltCommandResponseBuilderFactory>()
            .AddScoped<IInitCommand, InitCommand>()
            .AddScoped<IInitCommandRequestBuilder, InitCommandRequestBuilder>()
            .AddScoped<IInitCommandRequestBuilderFactory, InitCommandRequestBuilderFactory>()
            .AddScoped<IInitCommandResponseBuilder, InitCommandResponseBuilder>()
            .AddScoped<IInitCommandResponseBuilderFactory, InitCommandResponseBuilderFactory>()
            .AddScoped<ISshCommand, SshCommand>()
            .AddScoped<ISshCommandRequestBuilder, SshCommandRequestBuilder>()
            .AddScoped<ISshCommandRequestBuilderFactory, SshCommandRequestBuilderFactory>()
            .AddScoped<ISshCommandResponseBuilder, SshCommandResponseBuilder>()
            .AddScoped<ISshCommandResponseBuilderFactory, SshCommandResponseBuilderFactory>()
            .AddScoped<ISshConfigCommand, SshConfigCommand>()
            .AddScoped<ISshConfigCommandRequestBuilder, SshConfigCommandRequestBuilder>()
            .AddScoped<ISshConfigCommandRequestBuilderFactory, SshConfigCommandRequestBuilderFactory>()
            .AddScoped<ISshConfigCommandResponseBuilder, SshConfigCommandResponseBuilder>()
            .AddScoped<ISshConfigCommandResponseBuilderFactory, SshConfigCommandResponseBuilderFactory>()
            .AddScoped<IStatusCommand, StatusCommand>()
            .AddScoped<IStatusCommandRequestBuilder, StatusCommandRequestBuilder>()
            .AddScoped<IStatusCommandRequestBuilderFactory, StatusCommandRequestBuilderFactory>()
            .AddScoped<IStatusCommandResponseBuilder, StatusCommandResponseBuilder>()
            .AddScoped<IStatusCommandResponseBuilderFactory, StatusCommandResponseBuilderFactory>()
            .AddScoped<IUpCommand, UpCommand>()
            .AddScoped<IUpCommandRequestBuilder, UpCommandRequestBuilder>()
            .AddScoped<IUpCommandRequestBuilderFactory, UpCommandRequestBuilderFactory>()
            .AddScoped<IUpCommandResponseBuilder, UpCommandResponseBuilder>()
            .AddScoped<IUpCommandResponseBuilderFactory, UpCommandResponseBuilderFactory>()
            ;

        return services;
    }
}