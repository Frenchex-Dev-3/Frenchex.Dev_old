using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.SshConfig;

public class SshConfigCommandResponseBuilder : RootResponseBuilder, ISshConfigCommandResponseBuilder
{
    public ISshConfigCommandResponse Build()
    {
        return new SshConfigCommandResponse();
    }
}