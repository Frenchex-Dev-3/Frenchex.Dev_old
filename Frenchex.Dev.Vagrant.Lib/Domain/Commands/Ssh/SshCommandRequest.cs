using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Ssh;

public class SshCommandRequest : RootCommandRequest, ISshCommandRequest
{
    public SshCommandRequest(
        string nameOrId,
        string command,
        bool plain,
        string extraSshArgs,
        IBaseCommandRequest baseRequest
    ) : base(baseRequest)
    {
        NameOrId = nameOrId;
        Command = command;
        Plain = plain;
        ExtraSshArgs = extraSshArgs;
    }

    public string NameOrId { get; }
    public string Command { get; }
    public bool Plain { get; }
    public string ExtraSshArgs { get; }
}