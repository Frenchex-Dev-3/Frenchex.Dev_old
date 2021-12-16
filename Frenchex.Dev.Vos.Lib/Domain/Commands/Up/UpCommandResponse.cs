using Frenchex.Dev.Dotnet.Process.Lib.Domain.Process;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Up
{
    public class UpCommandResponse : RootResponse, IUpCommandResponse
    {
        public UpCommandResponse(
            Vagrant.Lib.Domain.Commands.Up.IUpCommandResponse upCommandResponse
        )
        {
            Response = upCommandResponse;
        }

        public Vagrant.Lib.Domain.Commands.Up.IUpCommandResponse Response { get; private set; }
    }
}
