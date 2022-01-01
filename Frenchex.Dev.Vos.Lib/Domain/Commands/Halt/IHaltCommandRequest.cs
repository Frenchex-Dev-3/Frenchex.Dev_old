using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Halt;

public interface IHaltCommandRequest : IRootCommandRequest
{
    string[] Names { get; }
    bool Force { get; }
    int HaltTimeoutInMiliSeconds { get; }
}