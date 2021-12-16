namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Destroy
{
    public interface IDestroyCommandResponseBuilder : Root.IRootCommandResponseBuilder
    {
        IDestroyCommandResponse Build();
    }
}
