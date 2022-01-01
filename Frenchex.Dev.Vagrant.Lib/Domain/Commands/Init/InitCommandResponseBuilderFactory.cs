using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Init;

public class InitCommandResponseBuilderFactory : RootCommandResponseBuilderFactory, IInitCommandResponseBuilderFactory
{
    public IInitCommandResponseBuilder Build()
    {
        return new InitCommandResponseBuilder();
    }
}