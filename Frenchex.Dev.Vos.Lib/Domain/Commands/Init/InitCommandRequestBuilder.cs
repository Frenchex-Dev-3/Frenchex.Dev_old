using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Init;

public class InitCommandRequestBuilder : RootCommandRequestBuilder, IInitCommandRequestBuilder
{
    private int? _leadingZeroes;

    private string? _namingPattern;

    public InitCommandRequestBuilder(IBaseRequestBuilderFactory baseRequestBuilderFactory) : base(
        baseRequestBuilderFactory)
    {
    }

    public IInitCommandRequest Build()
    {
        return new InitCommandRequest(
            BaseBuilder.Build(),
            _namingPattern ?? "",
            _leadingZeroes ?? 1
        );
    }

    public IInitCommandRequestBuilder WithGivenLeadingZeroes(int leadingZeroes)
    {
        _leadingZeroes = leadingZeroes;
        return this;
    }

    public IInitCommandRequestBuilder WithNamingPattern(string namingPattern)
    {
        _namingPattern = namingPattern;
        return this;
    }
}