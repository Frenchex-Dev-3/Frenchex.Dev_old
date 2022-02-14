using Frenchex.Dev.Dotnet.Cli.Integration.Lib.Domain;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Define;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Define.Machine;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Define.Machine.Add;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Define.MachineType;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Define.MachineType.Add;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Destroy;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Halt;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Init;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Name;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Ssh;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.SshConfig;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Status;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Up;
using Microsoft.Extensions.DependencyInjection;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.DependencyInjection;

public class ServicesConfiguration
{
    public static void ConfigureServices(IServiceCollection services)
    {
        Vos.Lib.DependencyInjection.ServicesConfiguration
            .ConfigureServices(services);

        services
            .AddScoped<IIntegration, Domain.Integration>();
            ;

        /**
        * Integration classes are meant to be used only once during execution of CLI.
        * Marking them as Singleton will save their unique instance into the DI.
        * While we only need them once.
        * So we mark them as Transient so that created instances will not be managed
        * by DI.
        **/


        services
            .AddScoped<IVosCommandIntegration, DefineCommandIntegration>()
            .AddScoped<IDefineCommandIntegration, DefineCommandIntegration>()
            
            .AddScoped<IDefineMachineCommandIntegration, DefineMachineCommandIntegration>()
            .AddScoped<IDefineSubCommandIntegration, DefineMachineCommandIntegration>()
            .AddScoped<IDefineMachineSubCommandIntegration, DefineMachineAddCommandIntegration>()
            
            .AddScoped<IDefineSubCommandIntegration, DefineMachineTypeCommandIntegration>()
            .AddScoped<IDefineMachineTypeSubCommandIntegration, DefineMachineTypeAddCommandIntegration>()
            
            .AddScoped<IVosCommandIntegration, DestroyCommandIntegration>()
            .AddScoped<IDestroyCommandIntegration, DestroyCommandIntegration>()
            
            .AddScoped<IVosCommandIntegration, HaltCommandIntegration>()
            .AddScoped<IHaltCommandIntegration, HaltCommandIntegration>()
            
            .AddScoped<IVosCommandIntegration, InitCommandIntegration>()
            .AddScoped<IInitCommandIntegration, InitCommandIntegration>()
            
            .AddScoped<IVosCommandIntegration, SshCommandIntegration>()
            .AddScoped<ISshCommandIntegration, SshCommandIntegration>()
            
            .AddScoped<IVosCommandIntegration, SshConfigCommandIntegration>()
            .AddScoped<ISshConfigCommandIntegration, SshConfigCommandIntegration>()
            
            .AddScoped<IVosCommandIntegration, UpCommandIntegration>()
            .AddScoped<IUpCommandIntegration, UpCommandIntegration>()

            .AddScoped<IVosCommandIntegration, StatusCommandIntegration>()
            .AddScoped<IStatusCommandIntegration, StatusCommandIntegration>()

            .AddScoped<IVosCommandIntegration, NameCommandIntegration>()
            .AddScoped<INameCommandIntegration, NameCommandIntegration>()
            ;
    }
}