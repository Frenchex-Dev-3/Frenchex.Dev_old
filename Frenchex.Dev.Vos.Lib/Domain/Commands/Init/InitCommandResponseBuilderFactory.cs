using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Init;

public class InitCommandResponseBuilderFactory : RootResponseBuilderFactory, IInitCommandResponseBuilderFactory
{
    public IInitCommandResponseBuilder Factory()
    {
        return new InitCommandResponseBuilder();
    }
}