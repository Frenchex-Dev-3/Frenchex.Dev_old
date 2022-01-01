using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Status;

public class StatusCommandRequest : IStatusCommandRequest
{
    public StatusCommandRequest(IBaseRequest @base, string[] namesOrIds)
    {
        Base = @base;
        Names = namesOrIds;
    }

    public IBaseRequest Base { get; }

    public string[] Names { get; }
}