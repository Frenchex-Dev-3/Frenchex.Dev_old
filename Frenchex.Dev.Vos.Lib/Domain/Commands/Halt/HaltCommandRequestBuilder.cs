using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Halt;

public class HaltCommandRequestBuilder : RootCommandRequestBuilder, IHaltCommandRequestBuilder
{
    private int? _usingHaltTimeoutMs;
    private string[]? _usingNamesOrIds;
    private bool? _withForce;
    private bool? _withGraceful;
    private bool? _withParallel;

    public HaltCommandRequestBuilder(IBaseRequestBuilderFactory baseRequestBuilderFactory) : base(
        baseRequestBuilderFactory)
    {
    }

    public IHaltCommandRequest Build()
    {
        return new HaltCommandRequest(
            _usingNamesOrIds ?? Array.Empty<string>(),
            _withForce ?? false,
            _withParallel ?? false,
            _withGraceful ?? false,
            _usingHaltTimeoutMs ?? (int) TimeSpan.FromMinutes(10).TotalMilliseconds,
            BaseBuilder.Build()
        );
    }

    public IHaltCommandRequestBuilder UsingNames(string[]? namesOrIds)
    {
        _usingNamesOrIds = namesOrIds;
        return this;
    }

    public IHaltCommandRequestBuilder WithForce(bool with)
    {
        _withForce = with;
        return this;
    }

    public IHaltCommandRequestBuilder UsingHaltTimeoutInMiliSeconds(int timeoutMs)
    {
        _usingHaltTimeoutMs = timeoutMs;
        return this;
    }

    public IHaltCommandRequestBuilder WithParallel(bool with)
    {
        _withParallel = with;
        return this;
    }

    public IHaltCommandRequestBuilder WithGraceful(bool with)
    {
        _withGraceful = with;
        return this;
    }
}