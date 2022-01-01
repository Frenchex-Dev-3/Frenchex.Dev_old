using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Destroy;

public interface IDestroyCommandRequest : IRootCommandRequest
{
    string Name { get; }
    bool Force { get; }
    bool Parallel { get; }
    bool Graceful { get; }
    int DestroyTimeoutInMiliSeconds { get; }
}