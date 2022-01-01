using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.Provisioning.Map;

public class DefineProvisioningMapCommandRequestBuilder : IDefineProvisioningMapCommandRequestBuilder
{
    private Dictionary<string, string>? _env;

    private string? _name;

    public DefineProvisioningMapCommandRequestBuilder(IBaseRequestBuilderFactory baseBuilderFactory)
    {
        BaseBuilder = baseBuilderFactory.Factory(this);
    }

    public IBaseRequestBuilder BaseBuilder { get; }

    public IDefineProvisioningMapCommandRequest Build()
    {
        if (null == BaseBuilder || null == _name || null == _env)
            throw new InvalidOperationException("basebuilder or name or env is null");

        return new DefineProvisioningMapCommandRequest(BaseBuilder.Build(), _name, _env);
    }

    public IDefineProvisioningMapCommandRequestBuilder UsingEnv(Dictionary<string, string> env)
    {
        _env = env;
        return this;
    }

    public IDefineProvisioningMapCommandRequestBuilder UsingProvisioning(string name)
    {
        _name = name;
        return this;
    }
}