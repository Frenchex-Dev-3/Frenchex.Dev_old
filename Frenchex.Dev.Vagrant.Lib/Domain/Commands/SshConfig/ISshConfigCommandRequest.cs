namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.SshConfig
{
    public interface ISshConfigCommandRequest : Root.IRootCommandRequest
    {
        string NameOrId { get; }
        string Host { get; }
    }
}
