namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.Provisioning.Map;

public class DefineProvisioningMapCommandResponseBuilderFactory : IDefineProvisioningMapCommandResponseBuilderFactory
{
    public IDefineProvisioningMapCommandResponseBuilder Factory()
    {
        return new DefineProvisioningMapCommandResponseBuilder();
    }
}