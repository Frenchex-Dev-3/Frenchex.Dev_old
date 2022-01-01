using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Status;

public class StatusCommandRequestBuilder : RootCommandRequestBuilder, IStatusCommandRequestBuilder
{
    private string[]? _namesOrIds;

    public StatusCommandRequestBuilder(IBaseRequestBuilderFactory baseRequestBuilderFactory) : base(
        baseRequestBuilderFactory)
    {
    }

    public IStatusCommandRequest Build()
    {
        _namesOrIds ??= Array.Empty<string>();

        return new StatusCommandRequest(
            BaseBuilder.Build(),
            _namesOrIds
        );
    }

    public IStatusCommandRequestBuilder WithNames(string[] namesOrIds)
    {
        _namesOrIds = namesOrIds;
        return this;
    }
}