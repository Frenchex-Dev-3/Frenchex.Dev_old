using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Up
{
    public class UpCommandRequest : RootRequest, IUpCommandRequest
    {
        public bool Provision { get; }
        public string[] ProvisionWith { get; }
        public bool DestroyOnError { get; }
        public bool Parallel { get; }
        public string Provider { get; }
        public bool InstallProvider { get; }
        public bool Minimal { get; }
        public string[] Names { get; }
        public UpCommandRequest(
               string[] namesOrIds,
               bool provision,
               string[] provisionWith,
               bool destroyOnError,
               bool parallel,
               string provider,
               bool installProvider,
               bool minimal,
               IBaseRequest baseRequest
           ) : base(baseRequest)
        {
            Names = namesOrIds;
            Provision = provision;
            ProvisionWith = provisionWith;
            DestroyOnError = destroyOnError;
            Parallel = parallel;
            Provider = provider;
            InstallProvider = installProvider;
            Minimal = minimal;
        }

    }

}
