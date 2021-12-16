namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Ssh
{
    public interface ISshCommandResponseBuilder : Root.IRootCommandResponseBuilder
    {
        ISshCommandResponse Build();
    }
}
