namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Ssh
{
    public interface ISshCommandRequest : Root.IRootCommandRequest
    {
        string NameOrId { get; }
        string Command { get; }
        bool Plain { get; }
        string ExtraSshArgs { get; }
    }
}
