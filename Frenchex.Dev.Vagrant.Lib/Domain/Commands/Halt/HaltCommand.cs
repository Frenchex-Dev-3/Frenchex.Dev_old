﻿using System.Text;
using Frenchex.Dev.Dotnet.Filesystem.Lib.Domain;
using Frenchex.Dev.Dotnet.Process.Lib.Domain.ProcessBuilder;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;
using Microsoft.Extensions.Configuration;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Halt;

public class HaltCommand : RootCommand, IHaltCommand
{
    private readonly IHaltCommandResponseBuilderFactory _responseBuilderFactory;

    public HaltCommand(
        IProcessBuilder processBuilder,
        IFilesystem fileSystem,
        IHaltCommandResponseBuilderFactory responseBuilderFactory,
        IConfiguration configuration
    ) : base(processBuilder, fileSystem, configuration)
    {
        _responseBuilderFactory = responseBuilderFactory;
    }

    public IHaltCommandResponse StartProcess(IHaltCommandRequest request)
    {
        var responseBuilder = _responseBuilderFactory.Build();

        BuildAndStartProcess(
            request,
            responseBuilder,
            BuildArguments(request)
        );

        return responseBuilder.Build();
    }

    private static string BuildArguments(IHaltCommandRequest request)
    {
        return GetCliCommandName() + " " + BuildVagrantOptions(request) + " " + BuildVagrantArguments(request);
    }

    private static string GetCliCommandName()
    {
        return "halt";
    }

    protected static string BuildVagrantOptions(IHaltCommandRequest request)
    {
        if (null == request.Base) throw new InvalidOperationException("request.Base is null");

        return new StringBuilder()
                .Append(request.Force ? " --force" : "")
                .Append(BuildRootVagrantOptions(request.Base))
                .ToString()
            ;
    }

    protected static string BuildVagrantArguments(IHaltCommandRequest request)
    {
        return request.NamesOrIds is {Length: > 0} ? string.Join(" ", request.NamesOrIds) : "";
    }
}