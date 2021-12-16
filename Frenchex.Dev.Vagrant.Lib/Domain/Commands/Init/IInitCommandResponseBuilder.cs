namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Init
{
    public interface IInitCommandResponseBuilder : Root.IRootCommandResponseBuilder
    {
        IInitCommandResponse Build();
    }
}
