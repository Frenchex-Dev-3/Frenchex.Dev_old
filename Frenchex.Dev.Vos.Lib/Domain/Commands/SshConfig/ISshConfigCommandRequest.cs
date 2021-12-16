using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.SshConfig
{
    public interface ISshConfigCommandRequest : IRootCommandRequest
    {
        string Name { get; }
        string Host { get; }
    }
}
