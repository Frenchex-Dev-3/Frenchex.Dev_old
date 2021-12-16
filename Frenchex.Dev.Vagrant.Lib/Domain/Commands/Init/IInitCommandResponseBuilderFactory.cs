namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Init
{
    public interface IInitCommandResponseBuilderFactory : Root.IRootCommandResponseBuilderFactory
    {
        IInitCommandResponseBuilder Build();
    }
}
