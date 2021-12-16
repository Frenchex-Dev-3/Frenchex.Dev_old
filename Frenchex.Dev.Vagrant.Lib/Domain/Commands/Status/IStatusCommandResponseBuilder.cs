namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Status
{
    public interface IStatusCommandResponseBuilder : Root.IRootCommandResponseBuilder
    {
        IStatusCommandResponse Build();
    }
}
