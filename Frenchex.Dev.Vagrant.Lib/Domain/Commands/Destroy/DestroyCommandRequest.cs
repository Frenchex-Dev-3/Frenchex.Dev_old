using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Destroy
{
    public class DestroyCommandRequest : RootCommandRequest, IDestroyCommandRequest
    {
        public string NameOrId { get; private set; }
        public bool Force { get; private set; }
        public bool Parallel { get; private set; }
        public bool Graceful { get; private set; }
        public int DestroyTimeoutInMiliSeconds { get; private set; }

        public DestroyCommandRequest(
            string nameOrId,
            bool force,
            bool parallel,
            bool graceful,
            int destroyTimeoutInMiliSeconds,
            IBaseCommandRequest baseRequest
        ) : base(baseRequest)
        {
            NameOrId = nameOrId;
            Force = force;
            Parallel = parallel;
            Graceful = graceful;
            DestroyTimeoutInMiliSeconds = destroyTimeoutInMiliSeconds;
        }
    }
}
