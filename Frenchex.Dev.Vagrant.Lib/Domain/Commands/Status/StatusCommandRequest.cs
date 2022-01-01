using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Status;

public class StatusCommandRequest : RootCommandRequest, IStatusCommandRequest
{
    public StatusCommandRequest(
        IBaseCommandRequest baseRequest,
        string[] namesOrIds
    ) : base(baseRequest)
    {
        NamesOrIds = namesOrIds;
    }

    public string[] NamesOrIds { get; }
}