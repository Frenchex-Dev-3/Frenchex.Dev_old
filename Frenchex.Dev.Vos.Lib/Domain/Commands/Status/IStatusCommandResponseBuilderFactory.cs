using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Status;

public interface IStatusCommandResponseBuilderFactory : IRootResponseBuilderFactory
{
    IStatusCommandResponseBuilder Factory();
}