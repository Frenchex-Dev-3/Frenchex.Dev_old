namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Up
{
    public interface IUpCommandResponseBuilderFactory : Root.IRootCommandResponseBuilderFactory
    {
        IUpCommandResponseBuilder Build();
    }
}
