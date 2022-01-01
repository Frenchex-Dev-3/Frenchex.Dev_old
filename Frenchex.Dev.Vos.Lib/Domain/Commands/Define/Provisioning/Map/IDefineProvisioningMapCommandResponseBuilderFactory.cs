using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.Provisioning.Map;

public interface IDefineProvisioningMapCommandResponseBuilderFactory : IRootResponseBuilderFactory
{
    IDefineProvisioningMapCommandResponseBuilder Factory();
}