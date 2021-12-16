namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Up
{
    public interface IUpCommandRequest : Root.IRootCommandRequest
    {
        string[] NamesOrIds { get; }
        bool Provision { get; }
        string[] ProvisionWith { get; }
        bool DestroyOnError { get; }
        bool Parallel { get; }
        string Provider { get; }
        bool InstallProvider { get; }
    }

}
