using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Ssh;

public interface ISshCommandResponseBuilderFactory : IRootResponseBuilderFactory
{
    ISshCommandResponseBuilder Build();
}