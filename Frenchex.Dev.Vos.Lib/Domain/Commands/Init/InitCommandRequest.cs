using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Init
{
    public class InitCommandRequest : RootRequest, IInitCommandRequest
    {
        public int InstanceNumber { get; }

        public string NamingPattern { get; }

        public InitCommandRequest(
            IBaseRequest baseRequest,
            int instanceNumber,
            string namingPatetrn
        ) : base(baseRequest)
        {
            InstanceNumber = instanceNumber;
            NamingPattern = namingPatetrn;
        }
    }
}
