namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root
{
    abstract public class RootCommandRequestBuilderFactory : IRootCommandRequestBuilderFactory
    {
        protected readonly IBaseCommandRequestBuilderFactory _baseRequestBuilderFactory;

        public RootCommandRequestBuilderFactory(
            IBaseCommandRequestBuilderFactory baseRequestBuilderFactory
        )
        {
            _baseRequestBuilderFactory = baseRequestBuilderFactory;
        }
    }
}
