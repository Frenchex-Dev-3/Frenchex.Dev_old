using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Destroy;

public interface IDestroyCommandRequestBuilder : IRootCommandRequestBuilder
{
    IDestroyCommandRequestBuilder UsingName(string? name);
    IDestroyCommandRequestBuilder WithForce(bool force);
    IDestroyCommandRequestBuilder WithParallel(bool parallel);
    IDestroyCommandRequestBuilder WithGraceful(bool graceful);
    IDestroyCommandRequestBuilder UsingDestroyTimeoutMiliseconds(int timeoutMs);
    IDestroyCommandRequest Build();
}