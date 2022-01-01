using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Status;

public class StatusCommandRequestBuilder : RootCommandRequestBuilder, IStatusCommandRequestBuilder
{
    private string[]? _namesOrIds;

    public StatusCommandRequestBuilder(
        IBaseCommandRequestBuilderFactory? baseRequestBuilderFactory
    ) : base(baseRequestBuilderFactory)
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

    public IStatusCommandRequestBuilder WithNamesOrIds(string[] nameOrId)
    {
        _namesOrIds = nameOrId;
        return this;
    }
}