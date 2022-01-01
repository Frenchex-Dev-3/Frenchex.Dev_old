using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Ssh;

public class SshCommandResponseBuilder : RootResponseBuilder, ISshCommandResponseBuilder
{
    public ISshCommandResponse Build()
    {
        return new SshCommandResponse();
    }
}