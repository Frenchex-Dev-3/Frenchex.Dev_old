using Frenchex.Dev.Vos.Lib.Domain.Actions.Configuration.Create;
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
            .AddSingleton<IVagrantfileResource, VagrantfileResource>()
            .AddSingleton<IConfigJsonResource, ConfigJsonResource>()
            ;

        // actions

        services
            .AddTransient<IConfigurationCreateAction, ConfigurationCreateAction>()
            .AddTransient<IConfigurationLoadAction, ConfigurationLoadAction>()
            .AddTransient<IConfigurationSaveAction, ConfigurationSaveAction>()
            .AddTransient<IDefaultGatewayGetterAction, DefaultGatewayGetterAction>()
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
            .AddSingleton<IVexNameToVagrantNameConverter, VexNameToVagrantNameConverter>()

            // Commands
            .AddSingleton<INameCommand, NameCommand>()
            .AddTransient<INameCommandRequestBuilder, NameCommandRequestBuilder>()
            .AddSingleton<INameCommandRequestBuilderFactory, NameCommandRequestBuilderFactory>()
            .AddTransient<INameCommandResponseBuilder, NameCommandResponseBuilder>()
            .AddSingleton<INameCommandResponseBuilderFactory, NameCommandResponseBuilderFactory>()
            .AddSingleton<IDefineMachineAddCommand, DefineMachineAddCommand>()
            .AddTransient<IDefineMachineAddCommandRequestBuilder, DefineMachineAddCommandRequestBuilder>()
            .AddSingleton<IDefineMachineAddCommandRequestBuilderFactory, DefineMachineAddCommandRequestBuilderFactory>()
            .AddTransient<IDefineMachineAddCommandResponseBuilder, DefineMachineAddCommandResponseBuilder>()
            .AddSingleton<IDefineMachineAddCommandResponseBuilderFactory,
                DefineMachineAddCommandResponseBuilderFactory>()
            .AddSingleton<IDefineMachineTypeAddCommand, DefineMachineTypeAddCommand>()
            .AddTransient<IDefineMachineTypeAddCommandRequestBuilder, DefineMachineTypeAddCommandRequestBuilder>()
            .AddSingleton<IDefineMachineTypeAddCommandRequestBuilderFactory,
                DefineMachineTypeAddCommandRequestBuilderFactory>()
            .AddTransient<IDefineMachineTypeAddCommandResponseBuilder, DefineMachineTypeAddCommandResponseBuilder>()
            .AddSingleton<IDefineMachineTypeAddCommandResponseBuilderFactory,
                DefineMachineTypeAddCommandResponseBuilderFactory>()
            .AddSingleton<IDestroyCommand, DestroyCommand>()
            .AddSingleton<IDestroyCommandRequestBuilderFactory, DestroyCommandRequestBuilderFactory>()
            .AddSingleton<IDestroyCommandResponseBuilderFactory, DestroyCommandResponseBuilderFactory>()
            .AddSingleton<IHaltCommand, HaltCommand>()
            .AddSingleton<IHaltCommandRequestBuilderFactory, HaltCommandRequestBuilderFactory>()
            .AddSingleton<IHaltCommandResponseBuilderFactory, HaltCommandResponseBuilderFactory>()
            .AddSingleton<IInitCommand, InitCommand>()
            .AddSingleton<IInitCommandRequestBuilderFactory, InitCommandRequestBuilderFactory>()
            .AddSingleton<IInitCommandResponseBuilderFactory, InitCommandResponseBuilderFactory>()
            .AddSingleton<ISshCommand, SshCommand>()
            .AddSingleton<ISshCommandRequestBuilderFactory, SshCommandRequestBuilderFactory>()
            .AddSingleton<ISshCommandResponseBuilderFactory, SshCommandResponseBuilderFactory>()
            .AddSingleton<ISshConfigCommand, SshConfigCommand>()
            .AddSingleton<ISshConfigCommandRequestBuilderFactory, SshConfigCommandRequestBuilderFactory>()
            .AddSingleton<ISshConfigCommandResponseBuilderFactory, SshConfigCommandResponseBuilderFactory>()
            .AddSingleton<IStatusCommand, StatusCommand>()
            .AddSingleton<IStatusCommandRequestBuilderFactory, StatusCommandRequestBuilderFactory>()
            .AddSingleton<IStatusCommandResponseBuilderFactory, StatusCommandResponseBuilderFactory>()
            .AddSingleton<IUpCommand, UpCommand>()
            .AddSingleton<IUpCommandRequestBuilderFactory, UpCommandRequestBuilderFactory>()
            .AddSingleton<IUpCommandResponseBuilderFactory, UpCommandResponseBuilderFactory>()
            ;

        return services;
    }
}