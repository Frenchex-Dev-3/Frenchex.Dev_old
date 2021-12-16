using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Halt
{
    public class HaltCommandRequest : RootCommandRequest, IHaltCommandRequest
    {
        public string[] NamesOrIds { get; }
        public bool Force { get; }
        public bool Parallel { get; }
        public bool Graceful { get; }
        public int HaltTimeoutInMiliSeconds { get; }

        public HaltCommandRequest(
            string[] namesOrIds,
            bool force,
            bool parallel,
            bool graceful,
            int haltTimeoutMs,
            IBaseCommandRequest baseRequest
        ) : base(baseRequest)
        {
            NamesOrIds = namesOrIds;
            Force = force;
            Parallel = parallel;
            Graceful = graceful;
            HaltTimeoutInMiliSeconds = haltTimeoutMs;
        }
    }
}
