namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Halt
{
    public interface IHaltCommandResponseBuilderFactory : Root.IRootCommandResponseBuilderFactory
    {
        IHaltCommandResponseBuilder Build();
    }
}
