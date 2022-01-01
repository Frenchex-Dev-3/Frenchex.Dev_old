namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

public class BaseCommandRequestBuilderFactory : IBaseCommandRequestBuilderFactory
{
    public IBaseCommandRequestBuilder Factory(object parent)
    {
        return new BaseCommandRequestBuilder(parent);
    }
}