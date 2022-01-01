using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Destroy;

public interface IDestroyCommandResponseBuilder : IRootResponseBuilder
{
    IDestroyCommandResponse Build();
}