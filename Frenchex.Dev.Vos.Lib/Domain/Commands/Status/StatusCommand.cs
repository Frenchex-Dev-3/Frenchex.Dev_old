using System.Collections.Immutable;
using Frenchex.Dev.Vagrant.Lib.Domain;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Configuration.Load;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Naming;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Status;

public class StatusCommand : RootCommand, IStatusCommand
{
    private readonly IStatusCommandResponseBuilderFactory _statusCommandResponseBuilderFactory;
    private readonly Vagrant.Lib.Domain.Commands.Status.IStatusCommand _vagrantStatusCommand;

    private readonly Vagrant.Lib.Domain.Commands.Status.IStatusCommandRequestBuilderFactory
        _vagrantStatusCommandRequestBuilderFactory;

    public StatusCommand(
        IConfigurationLoadAction configurationLoadAction,
        IStatusCommandResponseBuilderFactory statusCommandResponseBuilderFactory,
        IVexNameToVagrantNameConverter nameConverter,
        Vagrant.Lib.Domain.Commands.Status.IStatusCommand statusCommand,
        Vagrant.Lib.Domain.Commands.Status.IStatusCommandRequestBuilderFactory statusCommandRequestBuilderFactory
    ) : base(configurationLoadAction, nameConverter)
    {
        _statusCommandResponseBuilderFactory = statusCommandResponseBuilderFactory;
        _vagrantStatusCommand = statusCommand;
        _vagrantStatusCommandRequestBuilderFactory = statusCommandRequestBuilderFactory;
    }

    public async Task<IStatusCommandResponse> Execute(IStatusCommandRequest request)
    {
        var process = _vagrantStatusCommand.StartProcess(
            _vagrantStatusCommandRequestBuilderFactory.Factory()
                .BaseBuilder
                .UsingWorkingDirectory(request.Base.WorkingDirectory)
                .Parent<Vagrant.Lib.Domain.Commands.Status.IStatusCommandRequestBuilder>()
                .WithNamesOrIds(MapNamesToVagrantNames(
                        request.Names,
                        request.Base.WorkingDirectory,
                        await ConfigurationLoad(request.Base.WorkingDirectory)
                    )
                )
                .Build()
        );


        if (null == process.ProcessExecutionResult?.WaitForCompleteExit
            || null == process.ProcessExecutionResult?.OutputStream
           )
            throw new InvalidOperationException("wait for complete exit is null");

        await process.ProcessExecutionResult.WaitForCompleteExit;

        process.ProcessExecutionResult.OutputStream.Position = 0;
        var reader = new StreamReader(process.ProcessExecutionResult.OutputStream);

        var statusesOutput = (await reader.ReadToEndAsync())
            .Split("\r\n")
            .Skip(2) // vagrant header
            .Reverse()
            .Skip(5) // vagrant footer
            .Reverse()
            .Where(x => !string.IsNullOrEmpty(x)) // only not empty lines
            .ToList();

        var statuses = new Dictionary<string, (string, VagrantMachineStatusEnum)>();

        foreach (var item in statusesOutput)
        {
            var statusLineSplit = item
                .Split(" ")
                .ToList();
            var machine = statusLineSplit
                .First()
                .Trim();
            var statusString = (statusLineSplit[^3] + " " + statusLineSplit[^2].Trim())
                .Trim();
            var providerString = statusLineSplit[^1]
                .Replace("(", "")
                .Replace(")", "")
                .Trim();

            var status = VagrantMachineStatusEnum.NotCreated;

            switch (statusString)
            {
                case "not created":
                    status = VagrantMachineStatusEnum.NotCreated;
                    break;
                case "running":
                    status = VagrantMachineStatusEnum.Running;
                    break;
                case "aborted":
                    status = VagrantMachineStatusEnum.Aborted;
                    break;
                case "suspended":
                    status = VagrantMachineStatusEnum.Suspended;
                    break;
                case "stopped":
                    status = VagrantMachineStatusEnum.Stopped;
                    break;
            }

            statuses.Add(machine, (providerString, status));
        }

        return _statusCommandResponseBuilderFactory.Factory()
            .WithStatuses(statuses.ToImmutableDictionary())
            .Build();
    }
}