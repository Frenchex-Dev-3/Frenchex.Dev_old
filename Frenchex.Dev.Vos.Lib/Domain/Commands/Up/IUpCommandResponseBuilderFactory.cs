using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Up;

public interface IUpCommandResponseBuilderFactory : IRootResponseBuilderFactory
{
    IUpCommandResponseBuilder Factory();
}