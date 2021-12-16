namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Up
{
    public interface IUpCommandRequest : Root.IRootCommandRequest
    {
        string[] Names { get; }
        bool Provision { get; }
        string[] ProvisionWith { get; }
        bool DestroyOnError { get; }
        bool Parallel { get; }
        string Provider { get; }
        bool InstallProvider { get; }
    }

}
