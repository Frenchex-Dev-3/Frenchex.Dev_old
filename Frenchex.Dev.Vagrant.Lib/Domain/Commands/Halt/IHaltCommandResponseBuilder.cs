namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Halt
{
    public interface IHaltCommandResponseBuilder : Root.IRootCommandResponseBuilder
    {
        IHaltCommandResponse Build();
    }
}
