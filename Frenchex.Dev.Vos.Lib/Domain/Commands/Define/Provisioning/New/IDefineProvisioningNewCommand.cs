using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.Provisioning.New;

public interface
    IDefineProvisioningNewCommand : IRootCommand<IDefineProvisioningNewCommandRequest,
        IDefineProvisioningNewCommandResponse>
{
}