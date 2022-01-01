namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

public interface IBaseCommandRequestBuilderFactory
{
    IBaseCommandRequestBuilder Factory(object parent);
}