using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.SshConfig;

public class SshConfigCommandRequest : RootRequest, ISshConfigCommandRequest
{
    public SshConfigCommandRequest(
        string nameOrId,
        string host,
        IBaseRequest baseRequest
    ) : base(baseRequest)
    {
        Name = nameOrId;
        Host = host;
    }

    public string Name { get; }
    public string Host { get; }
}