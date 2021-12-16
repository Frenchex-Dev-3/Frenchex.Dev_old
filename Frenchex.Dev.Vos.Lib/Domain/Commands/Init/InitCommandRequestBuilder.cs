using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Init
{
    public class InitCommandRequestBuilder : RootCommandRequestBuilder, IInitCommandRequestBuilder
    {

        public InitCommandRequestBuilder(IBaseRequestBuilderFactory baseRequestBuilderFactory) : base(baseRequestBuilderFactory)
        {
        }

        public IInitCommandRequest Build()
        {
            return new InitCommandRequest(
                BaseBuilder.Build(),
                _instanceNumber ?? 0,
                _namingPattern ?? ""
            );
        }

        private int? _instanceNumber;
        public IInitCommandRequestBuilder WithInstanceNumber(int instanceNumber)
        {
            _instanceNumber = instanceNumber;
            return this;
        }

        private string? _namingPattern;
        public IInitCommandRequestBuilder WithNamingPattern(string namingPattern)
        {
            _namingPattern = namingPattern;
            return this;
        }
    }

}
