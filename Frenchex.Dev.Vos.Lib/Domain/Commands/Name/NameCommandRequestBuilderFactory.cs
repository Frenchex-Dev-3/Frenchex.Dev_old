using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Name;

public class NameCommandRequestBuilderFactory : RootCommandRequestBuilderFactory, INameCommandRequestBuilderFactory
{
    public NameCommandRequestBuilderFactory(
        IBaseRequestBuilderFactory baseRequestBuilderFactory
    ) : base(baseRequestBuilderFactory)
    {
    }

    public INameCommandRequestBuilder Factory()
    {
        return new NameCommandRequestBuilder(BaseRequestBuilderFactory);
    }
}