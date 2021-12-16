namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Status
{
    public interface IStatusCommandResponseBuilderFactory : Root.IRootCommandResponseBuilderFactory
    {
        IStatusCommandResponseBuilder Build();
    }
}
