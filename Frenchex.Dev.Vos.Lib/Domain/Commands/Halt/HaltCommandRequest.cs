using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Halt;

public class HaltCommandRequest : RootRequest, IHaltCommandRequest
{
    public HaltCommandRequest(
        string[] namesOrIds,
        bool force,
        bool parallel,
        bool graceful,
        int haltTimeoutMs,
        IBaseRequest baseRequest
    ) : base(baseRequest)
    {
        Names = namesOrIds;
        Force = force;
        Parallel = parallel;
        Graceful = graceful;
        HaltTimeoutInMiliSeconds = haltTimeoutMs;
    }

    public bool Parallel { get; }
    public bool Graceful { get; }
    public string[] Names { get; }
    public bool Force { get; }
    public int HaltTimeoutInMiliSeconds { get; }
}