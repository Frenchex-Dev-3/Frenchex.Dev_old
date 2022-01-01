using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Ssh;

public interface ISshCommandRequest : IRootCommandRequest
{
    string Name { get; }
    string Command { get; }
    bool Plain { get; }
    string ExtraSshArgs { get; }
}