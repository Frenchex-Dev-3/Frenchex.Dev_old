using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Init;

public interface IInitCommand : IRootCommand<IInitCommandRequest, IInitCommandResponse>
{
}