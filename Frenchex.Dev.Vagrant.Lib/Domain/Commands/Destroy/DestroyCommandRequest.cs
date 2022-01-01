using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Destroy;

public class DestroyCommandRequest : RootCommandRequest, IDestroyCommandRequest
{
    public DestroyCommandRequest(
        string nameOrId,
        bool force,
        bool parallel,
        bool graceful,
        int destroyTimeoutInMiliSeconds,
        IBaseCommandRequest baseRequest
    ) : base(baseRequest)
    {
        NameOrId = nameOrId;
        Force = force;
        Parallel = parallel;
        Graceful = graceful;
        DestroyTimeoutInMiliSeconds = destroyTimeoutInMiliSeconds;
    }

    public string NameOrId { get; }
    public bool Force { get; }
    public bool Parallel { get; }
    public bool Graceful { get; }
    public int DestroyTimeoutInMiliSeconds { get; }
}