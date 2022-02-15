using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Up;

public class UpCommandRequestBuilderFactory : RootCommandRequestBuilderFactory, IUpCommandRequestBuilderFactory
{
    public UpCommandRequestBuilderFactory(IBaseRequestBuilderFactory baseRequestBuilderFactory) : base(
        baseRequestBuilderFactory)
    {
    }

    public IUpCommandRequestBuilder Factory()
    {
        return new UpCommandRequestBuilder(BaseRequestBuilderFactory);
    }
}