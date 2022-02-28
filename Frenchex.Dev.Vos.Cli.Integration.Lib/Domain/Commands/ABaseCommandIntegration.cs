using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands;

public class ABaseCommandIntegration
{
    protected readonly ITimeoutMsOptionBuilder? TimeoutMsOptionBuilder;
    protected readonly IWorkingDirectoryOptionBuilder? WorkingDirectoryOptionBuilder;
    protected readonly IVagrantBinPathOptionBuilder? VagrantBinPathOptionBuilder;

    public ABaseCommandIntegration(
        IWorkingDirectoryOptionBuilder? workingDirectoryOptionBuilder,
        ITimeoutMsOptionBuilder? timeoutMsOptionBuilder,
        IVagrantBinPathOptionBuilder? vagrantBinPathOptionBuilder
    )
    {
        WorkingDirectoryOptionBuilder = workingDirectoryOptionBuilder;
        TimeoutMsOptionBuilder = timeoutMsOptionBuilder;
        VagrantBinPathOptionBuilder = vagrantBinPathOptionBuilder;
    }

    protected static void BuildBase(IRootCommandRequestBuilder requestBuilder, CommandIntegrationPayload payload)
    {
        requestBuilder
            .BaseBuilder
            .UsingWorkingDirectory(payload.WorkingDirectory)
            .UsingTimeoutMiliseconds(payload.TimeoutMs)
            .UsingVagrantBinPath(payload.VagrantBinPath)
            ;
    }
}