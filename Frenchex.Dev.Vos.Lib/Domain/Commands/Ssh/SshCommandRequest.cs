using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Ssh
{
    public class SshCommandRequest : RootRequest, ISshCommandRequest
    {
        public string Name { get; }
        public string Command { get; }
        public bool Plain { get; }
        public string ExtraSshArgs { get; }

        public SshCommandRequest(
           string name,
           string command,
           bool plain,
           string extraSshArgs,
           IBaseRequest baseRequest
       ) : base(baseRequest)
        {
            Name = name;
            Command = command;
            Plain = plain;
            ExtraSshArgs = extraSshArgs;
        }
    }
}
