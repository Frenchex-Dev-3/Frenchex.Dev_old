using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Up;

public interface IUpCommandRequest : IRootCommandRequest
{
    string[] Names { get; }
    bool Provision { get; }
    string[] ProvisionWith { get; }
    bool DestroyOnError { get; }
    bool Parallel { get; }
    string Provider { get; }
    bool InstallProvider { get; }
    int ParallelWorkers { get; }
    int ParallelWait { get; }

    IUpCommandRequest CloneWithNewNames(string[] names);
}