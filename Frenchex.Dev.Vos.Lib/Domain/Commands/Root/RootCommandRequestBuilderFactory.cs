namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Root
{
    abstract public class RootCommandRequestBuilderFactory : IRootCommandRequestBuilderFactory
    {
        protected readonly IBaseRequestBuilderFactory _baseRequestBuilderFactory;

        public RootCommandRequestBuilderFactory(
            IBaseRequestBuilderFactory baseRequestBuilderFactory
        )
        {
            _baseRequestBuilderFactory = baseRequestBuilderFactory;
        }
    }
}
