namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Ssh
{
    public interface ISshCommandRequest : Root.IRootCommandRequest
    {
        string Name { get; }
        string Command { get; }
        bool Plain { get; }
        string ExtraSshArgs { get; }
    }
}
