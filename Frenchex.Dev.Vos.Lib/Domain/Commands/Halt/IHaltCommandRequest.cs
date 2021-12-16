namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Halt
{
    public interface IHaltCommandRequest : Root.IRootCommandRequest
    {
        string[] Names { get; }
        bool Force { get; }
        int HaltTimeoutInMiliSeconds { get; }
    }
}
