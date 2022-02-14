using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Destroy;

public class DestroyCommandRequestBuilder : RootCommandRequestBuilder, IDestroyCommandRequestBuilder
{
    private int? _withDestroyTimeoutMs;
    private bool? _withForce;
    private bool? _withGraceful;
    private string? _withName;
    private bool? _withParallel;

    public DestroyCommandRequestBuilder(IBaseRequestBuilderFactory baseRequestBuilderFactory) : base(
        baseRequestBuilderFactory)
    {
    }

    public IDestroyCommandRequest Build()
    {
        return new DestroyCommandRequest(
            _withName ?? "",
            _withForce ?? false,
            _withParallel ?? false,
            _withGraceful ?? false,
            _withDestroyTimeoutMs ?? (int) TimeSpan.FromMinutes(10).TotalMilliseconds,
            BaseBuilder.Build()
        );
    }

    public IDestroyCommandRequestBuilder UsingName(string? nameOrId)
    {
        _withName = nameOrId;
        return this;
    }

    public IDestroyCommandRequestBuilder UsingDestroyTimeoutMiliseconds(int timeoutMs)
    {
        _withDestroyTimeoutMs = timeoutMs;
        return this;
    }

    public IDestroyCommandRequestBuilder WithForce(bool force)
    {
        _withForce = force;
        return this;
    }

    public IDestroyCommandRequestBuilder WithGraceful(bool graceful)
    {
        _withGraceful = graceful;
        return this;
    }

    public IDestroyCommandRequestBuilder WithParallel(bool parallel)
    {
        _withParallel = parallel;
        return this;
    }
}