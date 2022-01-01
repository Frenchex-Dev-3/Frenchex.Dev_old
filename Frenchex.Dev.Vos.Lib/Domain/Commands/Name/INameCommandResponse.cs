using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Name;

public interface INameCommandResponse : IRootCommandResponse
{
    string[] Names { get; }
}