namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Destroy
{
    public interface IDestroyCommandRequestBuilderFactory : Root.IRootCommandRequestBuilderFactory
    {
        IDestroyCommandRequestBuilder Factory();
    }
}
