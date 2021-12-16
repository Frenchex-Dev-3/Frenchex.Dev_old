namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Halt
{
    public interface IHaltCommandRequest : Root.IRootCommandRequest
    {
        string[] NamesOrIds { get; }
        bool Force { get; }
        int HaltTimeoutInMiliSeconds { get; }
    }
}
