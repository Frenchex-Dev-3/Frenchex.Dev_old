namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Destroy
{
    public interface IDestroyCommandResponseBuilderFactory : Root.IRootCommandResponseBuilderFactory
    {
        IDestroyCommandResponseBuilder Build();
    }
}
