using Frenchex.Dev.Dotnet.Process.Lib.Domain.Process;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Init
{
    public class InitCommandResponse : RootResponse, IInitCommandResponse
    {
        public InitCommandResponse(
        ) : base()
        {
        }
    }
}
