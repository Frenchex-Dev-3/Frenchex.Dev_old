namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Ssh
{
    public interface ISshCommandResponseBuilderFactory : Root.IRootCommandResponseBuilderFactory
    {
        ISshCommandResponseBuilder Build();
    }
}
