namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Up
{
    public interface IUpCommandResponseBuilder : Root.IRootCommandResponseBuilder
    {
        IUpCommandResponse Build();
    }
}
