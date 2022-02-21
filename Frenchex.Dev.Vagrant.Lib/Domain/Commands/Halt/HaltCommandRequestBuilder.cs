using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Halt;

public class HaltCommandRequestBuilder : RootCommandRequestBuilder, IHaltCommandRequestBuilder
{
    private int? _usingHaltTimeoutMs;
    private string[]? _usingNamesOrIds;
    private bool? _withForce;

    public HaltCommandRequestBuilder(
        IBaseCommandRequestBuilderFactory baseRequestBuilderFactory
    ) : base(baseRequestBuilderFactory)
    {
    }

    public IHaltCommandRequest Build()
    {
        return new HaltCommandRequest(
            _usingNamesOrIds ?? Array.Empty<string>(),
            _withForce ?? false,
            _usingHaltTimeoutMs ?? (int) TimeSpan.FromMinutes(10).TotalMilliseconds,
            BaseBuilder.Build()
        );
    }

    public IHaltCommandRequestBuilder UsingNamesOrIds(string[] namesOrIds)
    {
        _usingNamesOrIds = namesOrIds;
        return this;
    }

    public IHaltCommandRequestBuilder UsingHaltTimeoutInMiliSeconds(int timeoutMs)
    {
        _usingHaltTimeoutMs = timeoutMs;
        return this;
    }

    public IHaltCommandRequestBuilder WithForce(bool with)
    {
        _withForce = with;
        return this;
    }
}