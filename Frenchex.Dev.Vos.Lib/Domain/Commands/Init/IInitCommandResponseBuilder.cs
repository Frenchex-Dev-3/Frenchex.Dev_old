using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Init;

public interface IInitCommandResponseBuilder : IRootResponseBuilder
{
    IInitCommandResponse Build();
}