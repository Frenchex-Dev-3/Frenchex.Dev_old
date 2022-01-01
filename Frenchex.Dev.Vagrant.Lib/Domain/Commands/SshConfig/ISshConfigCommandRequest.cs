using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.SshConfig;

public interface ISshConfigCommandRequest : IRootCommandRequest
{
    string NameOrId { get; }
    string Host { get; }
}