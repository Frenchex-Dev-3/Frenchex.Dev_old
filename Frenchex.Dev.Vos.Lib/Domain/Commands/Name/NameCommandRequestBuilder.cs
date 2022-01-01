using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Name;

public class NameCommandRequestBuilder : RootCommandRequestBuilder, INameCommandRequestBuilder
{
    private string[]? _names;

    public NameCommandRequestBuilder(IBaseRequestBuilderFactory baseRequestBuilderFactory) : base(
        baseRequestBuilderFactory)
    {
    }

    public INameCommandRequest Build()
    {
        if (null == _names) throw new InvalidOperationException("Names is null");

        return new NameCommandRequest(
            BaseBuilder.Build(),
            _names
        );
    }

    public INameCommandRequestBuilder WithNames(string[] names)
    {
        _names = names;
        return this;
    }
}