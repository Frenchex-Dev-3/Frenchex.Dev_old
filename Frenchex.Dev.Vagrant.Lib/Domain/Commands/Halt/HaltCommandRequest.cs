using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Halt;

public class HaltCommandRequest : RootCommandRequest, IHaltCommandRequest
{
    public HaltCommandRequest(
        string[] namesOrIds,
        bool force,
        int haltTimeoutMs,
        IBaseCommandRequest baseRequest
    ) : base(baseRequest)
    {
        NamesOrIds = namesOrIds;
        Force = force;
        HaltTimeoutInMiliSeconds = haltTimeoutMs;
    }

    public int HaltTimeoutInMiliSeconds { get; }

    public string[] NamesOrIds { get; }
    public bool Force { get; }
}