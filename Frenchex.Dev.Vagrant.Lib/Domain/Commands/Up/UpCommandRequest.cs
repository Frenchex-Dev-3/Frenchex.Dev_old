using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Up
{
    public class UpCommandRequest : RootCommandRequest, IUpCommandRequest
    {
        public bool Provision { get; }
        public string[] ProvisionWith { get; }
        public bool DestroyOnError { get; }
        public bool Parallel { get; }
        public string Provider { get; }
        public bool InstallProvider { get; }
        public bool Minimal { get; }
        public string[] NamesOrIds { get; }
        public UpCommandRequest(
               string[] namesOrIds,
               bool provision,
               string[] provisionWith,
               bool destroyOnError,
               bool parallel,
               string provider,
               bool installProvider,
               bool minimal,
               IBaseCommandRequest baseRequest
           ) : base(baseRequest)
        {
            NamesOrIds = namesOrIds;
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
