using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Ssh;

public interface ISshCommandRequest : IRootCommandRequest
{
    string NameOrId { get; }
    string Command { get; }
    bool Plain { get; }
    string ExtraSshArgs { get; }
}