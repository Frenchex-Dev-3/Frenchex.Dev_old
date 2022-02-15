using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Init;

public class InitCommandRequestBuilderFactory : RootCommandRequestBuilderFactory, IInitCommandRequestBuilderFactory
{
    public InitCommandRequestBuilderFactory(IBaseRequestBuilderFactory baseRequestBuilderFactory) : base(
        baseRequestBuilderFactory)
    {
    }

    public IInitCommandRequestBuilder Factory()
    {
        return new InitCommandRequestBuilder(BaseRequestBuilderFactory);
    }
}