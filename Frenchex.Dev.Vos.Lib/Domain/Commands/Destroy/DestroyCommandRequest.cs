using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Destroy;

public class DestroyCommandRequest : RootRequest, IDestroyCommandRequest
{
    public DestroyCommandRequest(
        string nameOrId,
        bool force,
        bool parallel,
        bool graceful,
        int destroyTimeoutInMiliSeconds,
        IBaseRequest baseRequest
    ) : base(baseRequest)
    {
        Name = nameOrId;
        Force = force;
        Parallel = parallel;
        Graceful = graceful;
        DestroyTimeoutInMiliSeconds = destroyTimeoutInMiliSeconds;
    }

    public string Name { get; }
    public bool Force { get; }
    public bool Parallel { get; }
    public bool Graceful { get; }
    public int DestroyTimeoutInMiliSeconds { get; }
}