using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Name
{
    public class NameCommandRequestBuilder : RootCommandRequestBuilder, INameCommandRequestBuilder
    {
        public NameCommandRequestBuilder(IBaseRequestBuilderFactory baseRequestBuilderFactory) : base(baseRequestBuilderFactory)
        {
        }

        public INameCommandRequest Build()
        {
            if (null == _names)
            {
                throw new InvalidOperationException("Names is null");
            }

            return new NameCommandRequest(
                BaseBuilder.Build(),
                _names
            );
        }

        private string[]? _names;
        public INameCommandRequestBuilder WithNames(string[] names)
        {
            _names = names;
            return this;
        }
    }
}
