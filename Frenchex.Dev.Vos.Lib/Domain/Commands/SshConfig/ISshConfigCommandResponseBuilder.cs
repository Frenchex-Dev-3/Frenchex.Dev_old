namespace Frenchex.Dev.Vos.Lib.Domain.Commands.SshConfig
{
    public interface ISshConfigCommandResponseBuilder : Root.IRootResponseBuilder
    {
        ISshConfigCommandResponse Build();
    }
}
