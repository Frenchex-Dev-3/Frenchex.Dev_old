﻿using System.CommandLine;
using System.CommandLine.Binding;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Name;

public class NameCommandIntegrationPayloadBinder : BinderBase<NameCommandIntegrationPayload>
{
    private readonly Argument<string[]> _nameArg;
    private readonly Option<string> _workingDirOpt;
    private readonly Option<int> _timeoutOpt;

    public NameCommandIntegrationPayloadBinder(
        Argument<string[]> nameArg, 
        Option<string> workingDirOpt, 
        Option<int> timeoutOpt
    )
    {
        _nameArg = nameArg;
        _workingDirOpt = workingDirOpt;
        _timeoutOpt = timeoutOpt;
    }

    protected override NameCommandIntegrationPayload GetBoundValue(BindingContext bindingContext) =>
        new NameCommandIntegrationPayload()
        {
            WorkingDirectory = bindingContext.ParseResult.GetValueForOption(_workingDirOpt),
            TimeoutMs = bindingContext.ParseResult.GetValueForOption(_timeoutOpt),
            Names = bindingContext.ParseResult.GetValueForArgument(_nameArg)
        };
}