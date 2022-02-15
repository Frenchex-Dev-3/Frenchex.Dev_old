using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Halt;

public interface IHaltCommandRequest : IRootCommandRequest
{
    string[] NamesOrIds { get; }
    bool Force { get; }
}