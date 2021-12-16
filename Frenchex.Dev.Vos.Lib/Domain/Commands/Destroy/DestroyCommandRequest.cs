using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Destroy
{
    public class DestroyCommandRequest : RootRequest, IDestroyCommandRequest
    {
        public string Name { get; private set; }
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
            IBaseRequest baseRequest
        ) : base(baseRequest)
        {
            Name = nameOrId;
            Force = force;
            Parallel = parallel;
            Graceful = graceful;
            DestroyTimeoutInMiliSeconds = destroyTimeoutInMiliSeconds;
        }
    }
}
