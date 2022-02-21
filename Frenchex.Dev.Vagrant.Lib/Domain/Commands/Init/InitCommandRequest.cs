using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Init;

public class InitCommandRequest : RootCommandRequest, IInitCommandRequest
{
    public InitCommandRequest(
        string? boxVersion,
        bool? force,
        bool? minimal,
        string? outputToFile,
        string? templateFile,
        string? boxName,
        string? boxUrl,
        IBaseCommandRequest baseRequest
    ) : base(baseRequest)
    {
        BoxVersion = boxVersion;
        Force = force;
        Minimal = minimal;
        OutputToFile = outputToFile;
        TemplateFile = templateFile;
        BoxName = boxName;
        BoxUrl = boxUrl;
    }

    public string? BoxUrl { get; }

    public string? BoxVersion { get; }
    public bool? Force { get; }
    public bool? Minimal { get; }
    public string? OutputToFile { get; }
    public string? TemplateFile { get; }
    public string? BoxName { get; }
}