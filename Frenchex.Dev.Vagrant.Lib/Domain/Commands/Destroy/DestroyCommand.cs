using System.Text;
using Frenchex.Dev.Dotnet.Filesystem.Lib.Domain;
using Frenchex.Dev.Dotnet.Process.Lib.Domain.ProcessBuilder;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Destroy;

public class DestroyCommand : RootCommand, IDestroyCommand
{
    private readonly IDestroyCommandResponseBuilderFactory _responseBuilderFactory;

    public DestroyCommand(
        IProcessBuilder processExecutor,
        IFilesystem fileSystem,
        IDestroyCommandResponseBuilderFactory responseBuilderFactory
    ) : base(processExecutor, fileSystem)
    {
        _responseBuilderFactory = responseBuilderFactory;
    }

    public IDestroyCommandResponse StartProcess(IDestroyCommandRequest request)
    {
        var responseBuilder = _responseBuilderFactory.Build();

        BuildAndStartProcess(
            request,
            responseBuilder,
            BuildArguments(request)
        );

        return responseBuilder.Build();
    }

    private static string BuildArguments(IDestroyCommandRequest request)
    {
        return GetCliCommandName() + " " + BuildVagrantOptions(request) + " " + BuildVagrantArguments(request);
    }

    private static string GetCliCommandName()
    {
        return "destroy";
    }

    protected static string BuildVagrantOptions(IDestroyCommandRequest request)
    {
        if (null == request.Base) throw new InvalidOperationException("request.base is null");

        return new StringBuilder()
                .Append(request.Force ? " --force" : "")
                .Append(request.Parallel ? " --parallel" : "")
                .Append(request.Graceful ? " --graceful" : "")
                .Append(BuildRootVagrantOptions(request.Base))
                .ToString()
            ;
    }

    protected static string BuildVagrantArguments(IDestroyCommandRequest request)
    {
        return request.NameOrId;
    }

    protected static string BuildArguments(string command, IDestroyCommandRequest request)
    {
        return
            $"{command} " +
            $"{BuildVagrantOptions(request)} " +
            $"{BuildVagrantArguments(request)}"
            ;
    }
}