namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Init
{
    public interface IInitCommandRequest : Root.IRootCommandRequest
    {
        string? BoxName { get; }
        string? BoxUrl { get; }
        string? BoxVersion { get; }
        bool? Force { get; }
        bool? Minimal { get; }
        string? OutputToFile { get; }
        string? TemplateFile { get; }
    }
}
