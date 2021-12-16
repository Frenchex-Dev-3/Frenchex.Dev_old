namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Root
{
    public abstract class RootCommandRequestBuilder : IRootCommandRequestBuilder
    {
        public IBaseRequestBuilder BaseBuilder { get; private set; }
        public RootCommandRequestBuilder(
            IBaseRequestBuilderFactory baseRequestBuilderFactory
        )
        {
            BaseBuilder = baseRequestBuilderFactory.Factory(this);
        }
    }
}
