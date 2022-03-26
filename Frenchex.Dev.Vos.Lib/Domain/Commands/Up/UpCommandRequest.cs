using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Up;

public class UpCommandRequest : RootRequest, IUpCommandRequest
{
    public UpCommandRequest(
        string[] namesOrIds,
        bool provision,
        string[] provisionWith,
        bool destroyOnError,
        bool parallel,
        int parallelWorkers,
        int parallelWait,
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
        ParallelWorkers = parallelWorkers;
        ParallelWait = parallelWait;
    }

    public bool Minimal { get; }
    public bool Provision { get; }
    public string[] ProvisionWith { get; }
    public bool DestroyOnError { get; }
    public bool Parallel { get; }
    public string Provider { get; }
    public bool InstallProvider { get; }
    public int ParallelWorkers { get; }
    public int ParallelWait { get; }
    public string[] Names { get; }

    public IUpCommandRequest CloneWithNewNames(string[] names)
    {
        return new UpCommandRequest(
            names,
            Provision,
            ProvisionWith,
            DestroyOnError,
            Parallel,
            ParallelWorkers,
            ParallelWait,
            Provider,
            InstallProvider,
            Minimal,
            Base
        );
    }
}