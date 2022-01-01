using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Init;

public class InitCommandResponseBuilder : RootResponseBuilder, IInitCommandResponseBuilder
{
    public IInitCommandResponse Build()
    {
        return new InitCommandResponse();
    }
}