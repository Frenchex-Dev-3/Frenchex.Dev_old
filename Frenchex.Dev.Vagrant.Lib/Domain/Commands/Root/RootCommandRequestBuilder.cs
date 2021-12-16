namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root
{
    public abstract class RootCommandRequestBuilder : IRootCommandRequestBuilder
    {
        public IBaseCommandRequestBuilder BaseBuilder { get; private set; }
        public RootCommandRequestBuilder(
            IBaseCommandRequestBuilderFactory? baseRequestBuilderFactory
        )
        {
            if (null == baseRequestBuilderFactory)
            {
                throw new ArgumentNullException(nameof(baseRequestBuilderFactory));
            }

            BaseBuilder = baseRequestBuilderFactory.Factory(this);
        }
    }
}
