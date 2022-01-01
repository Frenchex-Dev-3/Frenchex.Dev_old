using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Halt;

public interface IHaltCommandResponseBuilderFactory : IRootResponseBuilderFactory
{
    IHaltCommandResponseBuilder Factory();
}