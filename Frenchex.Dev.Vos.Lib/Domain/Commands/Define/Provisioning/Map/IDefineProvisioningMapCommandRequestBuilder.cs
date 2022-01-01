using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.Provisioning.Map;

public interface IDefineProvisioningMapCommandRequestBuilder : IRootCommandRequestBuilder
{
    IDefineProvisioningMapCommandRequestBuilder UsingProvisioning(string name);
    IDefineProvisioningMapCommandRequestBuilder UsingEnv(Dictionary<string, string> env);
    IDefineProvisioningMapCommandRequest Build();
}