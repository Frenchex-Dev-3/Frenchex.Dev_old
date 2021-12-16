namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.SshConfig
{
    public interface ISshConfigCommandResponseBuilderFactory : Root.IRootCommandResponseBuilderFactory
    {
        ISshConfigCommandResponseBuilder Build();
    }
}
