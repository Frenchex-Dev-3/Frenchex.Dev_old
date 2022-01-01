using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.Provisioning.Map;

public class DefineProvisioningMapCommandRequestBuilderFactory : IDefineProvisioningMapCommandRequestBuilderFactory
{
    private readonly IBaseRequestBuilderFactory _baseRequestBuilderFactory;

    public DefineProvisioningMapCommandRequestBuilderFactory(
        IBaseRequestBuilderFactory baseRequestBuilderFactory
    )
    {
        _baseRequestBuilderFactory = baseRequestBuilderFactory;
    }

    public IDefineProvisioningMapCommandRequestBuilder NewInstance()
    {
        return new DefineProvisioningMapCommandRequestBuilder(_baseRequestBuilderFactory);
    }
}