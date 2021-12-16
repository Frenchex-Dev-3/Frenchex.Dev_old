namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Destroy
{
    public interface IDestroyCommandRequestBuilder : Root.IRootCommandRequestBuilder
    {
        IDestroyCommandRequestBuilder UsingName(string nameOrId);
        IDestroyCommandRequestBuilder WithForce(bool force);
        IDestroyCommandRequestBuilder WithParallel(bool parallel);
        IDestroyCommandRequestBuilder WithGraceful(bool graceful);
        IDestroyCommandRequestBuilder UsingDestroyTimeoutMiliseconds(int timeoutMs);
        IDestroyCommandRequest Build();
    }

}
