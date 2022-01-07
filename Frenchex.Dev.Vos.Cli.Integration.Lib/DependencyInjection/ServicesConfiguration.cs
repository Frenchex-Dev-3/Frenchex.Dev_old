using Frenchex.Dev.Dotnet.Cli.Integration.Lib.Domain;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Define;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Define.Machine;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Define.Machine.Add;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Define.MachineType;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Define.MachineType.Add;
using Microsoft.Extensions.DependencyInjection;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.DependencyInjection;

public class ServicesConfiguration
{
    public static void ConfigureServices(IServiceCollection services)
    {
        Vos.Lib.DependencyInjection.ServicesConfiguration
            .ConfigureServices(services);

        services
            .AddTransient<IIntegration, Domain.Integration>()
            ;

        /**
        * Integration classes are meant to be used only once during execution of CLI.
        * Marking them as Singleton will save their unique instance into the DI.
        * While we only need them once.
        * So we mark them as Transient so that created instances will not be managed
        * by DI.
        **/


        services
            .AddSingleton<IVosCommandIntegration, DefineCommandIntegration>()
            .AddSingleton<IDefineCommandIntegration, DefineCommandIntegration>()
            .AddSingleton<IDefineMachineCommandIntegration, DefineMachineCommandIntegration>()
            .AddSingleton<IDefineSubCommandIntegration, DefineMachineCommandIntegration>()
            .AddSingleton<IDefineMachineSubCommandIntegration, DefineMachineAddCommandIntegration>()
            .AddSingleton<IDefineSubCommandIntegration, DefineMachineTypeCommandIntegration>()
            .AddSingleton<IDefineMachineTypeSubCommandIntegration, DefineMachineTypeAddCommandIntegration>()
            .AddSingleton<IVosCommandIntegration, DestroyCommandIntegration>()
            .AddSingleton<IDestroyCommandIntegration, DestroyCommandIntegration>()
            .AddSingleton<IVosCommandIntegration, HaltCommandIntegration>()
            .AddSingleton<IHaltCommandIntegration, HaltCommandIntegration>()
            .AddSingleton<IVosCommandIntegration, InitCommandIntegration>()
            .AddSingleton<IInitCommandIntegration, InitCommandIntegration>()
            .AddSingleton<IVosCommandIntegration, SshCommandIntegration>()
            .AddSingleton<ISshCommandIntegration, SshCommandIntegration>()
            .AddSingleton<IVosCommandIntegration, SshConfigCommandIntegration>()
            .AddSingleton<ISshConfigCommandIntegration, SshConfigCommandIntegration>()
            .AddSingleton<IVosCommandIntegration, UpCommandIntegration>()
            .AddSingleton<IUpCommandIntegration, UpCommandIntegration>()
            .AddSingleton<IVosCommandIntegration, StatusCommandIntegration>()
            .AddSingleton<IStatusCommandIntegration, StatusCommandIntegration>()
            .AddSingleton<INameCommandIntegration, NameCommandIntegration>()
            ;
    }
}