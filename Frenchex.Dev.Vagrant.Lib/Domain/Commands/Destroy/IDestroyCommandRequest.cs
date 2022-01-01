using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Destroy;

public interface IDestroyCommandRequest : IRootCommandRequest
{
    string NameOrId { get; }
    bool Force { get; }
    bool Parallel { get; }
    bool Graceful { get; }
    int DestroyTimeoutInMiliSeconds { get; }
}