using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.SshConfig
{
    public class SshConfigCommandRequest : RootCommandRequest, ISshConfigCommandRequest
    {
        public string NameOrId { get; }
        public string Host { get; }

        public SshConfigCommandRequest(
          string nameOrId,
          string host,
          IBaseCommandRequest baseRequest
        ) : base(baseRequest)
        {
            NameOrId = nameOrId;
            Host = host;
        }
    }
}
