using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Name;

public class NameCommandRequest : RootRequest, INameCommandRequest
{
    public NameCommandRequest(
        IBaseRequest @base,
        string[] names
    ) : base(@base)
    {
        Names = names;
    }

    public string[] Names { get; }
}