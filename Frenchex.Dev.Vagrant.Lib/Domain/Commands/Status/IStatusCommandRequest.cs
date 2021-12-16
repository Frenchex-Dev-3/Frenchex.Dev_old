namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Status
{
    public interface IStatusCommandRequest : Root.IRootCommandRequest
    {
        string[] NamesOrIds { get; }
    }
}
