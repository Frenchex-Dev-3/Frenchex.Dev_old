using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Init;

public interface IInitCommandRequest : IRootCommandRequest
{
    string? BoxName { get; }
    string? BoxVersion { get; }
    bool? Force { get; }
    bool? Minimal { get; }
    string? OutputToFile { get; }
    string? TemplateFile { get; }
}