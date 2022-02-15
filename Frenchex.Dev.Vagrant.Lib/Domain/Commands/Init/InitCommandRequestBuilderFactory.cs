using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Init;

public class InitCommandRequestBuilderFactory : RootCommandRequestBuilderFactory, IInitCommandRequestBuilderFactory
{
    public InitCommandRequestBuilderFactory(
        IBaseCommandRequestBuilderFactory baseRequestBuilderFactory
    ) : base(baseRequestBuilderFactory)
    {
    }

    public IInitCommandRequestBuilder Factory()
    {
        return new InitCommandRequestBuilder(BaseRequestBuilderFactory);
    }
}