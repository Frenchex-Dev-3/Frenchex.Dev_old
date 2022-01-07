using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Frenchex.Dev.Vos.Cli.Integration.Lib.DependencyInjection;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Define;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Tests.Domain;

[TestClass]
public class WorkflowCommandsIntegrationTest
{
    #region Setup

    [TestInitialize]
    public void Setup()
    {
        var services = new ServiceCollection();

        ServicesConfiguration
            .ConfigureServices(services)
            ;

        var di = services.BuildServiceProvider();

        _initCommandIntegration = di.GetRequiredService<IInitCommandIntegration>();
        _defineCommandIntegration = di.GetRequiredService<IDefineCommandIntegration>();
        _upCommandIntegration = di.GetRequiredService<IUpCommandIntegration>();
        _sshConfigCommandIntegration = di.GetRequiredService<ISshConfigCommandIntegration>();
        _sshCommandIntegration = di.GetRequiredService<ISshCommandIntegration>();
        _haltCommandIntegration = di.GetRequiredService<IHaltCommandIntegration>();
        _destroyCommandIntegration = di.GetRequiredService<IDestroyCommandIntegration>();
        _statusCommandIntegration = di.GetRequiredService<IStatusCommandIntegration>();
        _nameCommandIntegration = di.GetRequiredService<INameCommandIntegration>();

        _rootCommand = new RootCommand();

        _destroyCommandIntegration.Integrate(_rootCommand);
        _defineCommandIntegration.Integrate(_rootCommand);
        _haltCommandIntegration.Integrate(_rootCommand);
        _initCommandIntegration.Integrate(_rootCommand);
        _sshConfigCommandIntegration.Integrate(_rootCommand);
        _sshCommandIntegration.Integrate(_rootCommand);
        _upCommandIntegration.Integrate(_rootCommand);
        _statusCommandIntegration.Integrate(_rootCommand);
        _nameCommandIntegration.Integrate(_rootCommand);
    }

    #endregion

    #region Privates

    private RootCommand? _rootCommand;
    private IInitCommandIntegration? _initCommandIntegration;
    private IDefineCommandIntegration? _defineCommandIntegration;
    private IUpCommandIntegration? _upCommandIntegration;
    private ISshConfigCommandIntegration? _sshConfigCommandIntegration;
    private ISshCommandIntegration? _sshCommandIntegration;
    private IHaltCommandIntegration? _haltCommandIntegration;
    private IDestroyCommandIntegration? _destroyCommandIntegration;
    private IStatusCommandIntegration? _statusCommandIntegration;
    private INameCommandIntegration? _nameCommandIntegration;

    #endregion

    #region Test_Complete_Workflow_Integration

    public static IEnumerable<object[]> Test_Data()
    {
        var timeOutOpt = "--timeoutms " + TimeSpan.FromMinutes(10).TotalMilliseconds;
        const string workingDirOpt = "--working-directory #WORKING_DIRECTORY#";

        yield return new object[]
        {
            new[]
            {
                $"init {timeOutOpt} {workingDirOpt}",
                $"define machine-type add foo 'generic/alpine38' 4 128 --enabled {timeOutOpt} {workingDirOpt}",
                $"define machine add foo foo 4 --enabled {timeOutOpt} {workingDirOpt}",
                $"name foo-[2-*] {timeOutOpt} {workingDirOpt}",
                $"status foo-[2-*] {timeOutOpt} {workingDirOpt}",
                $"up foo-0 {timeOutOpt} {workingDirOpt}",
                $"up foo-[2-*] {timeOutOpt} {workingDirOpt}",
                $"status foo-[2-*] {timeOutOpt} {workingDirOpt}",
                $"halt foo-[2-*] {timeOutOpt} {workingDirOpt}",
                $"destroy foo-[2-*] --force {timeOutOpt} {workingDirOpt}",
                $"destroy --force {timeOutOpt} {workingDirOpt}"
            }
        };

        yield return new object[]
        {
            new[]
            {
                $"init {timeOutOpt} {workingDirOpt}",
                $"define machine-type add foo 'generic/alpine38' 4 128 --enabled {timeOutOpt} {workingDirOpt}",
                $"define machine-type add bar 'generic/alpine38' 4 128 --enabled {timeOutOpt} {workingDirOpt}",
                $"define machine add foo foo 4 --enabled {timeOutOpt} {workingDirOpt}",
                $"define machine add bar bar 4 --enabled {timeOutOpt} {workingDirOpt}",
                $"name bar-0 foo-[2-*] {timeOutOpt} {workingDirOpt}",
                $"status bar-* foo-[2-*] {timeOutOpt} {workingDirOpt}",
                $"up foo-0 {timeOutOpt} {workingDirOpt}",
                $"up foo-[2-*] {timeOutOpt} {workingDirOpt}",
                $"status bar-* foo-[2-*] {timeOutOpt} {workingDirOpt}",
                $"halt bar-* foo-[2-*] {timeOutOpt} {workingDirOpt}",
                $"destroy bar-* foo-[2-*] --force {timeOutOpt} {workingDirOpt}",
                $"destroy --force {timeOutOpt} {workingDirOpt}"
            }
        };
    }

    [TestMethod]
    [DynamicData(nameof(Test_Data), DynamicDataSourceType.Method)]
    public async Task Test_Complete_Workflow_Integration(string[] commands)
    {
        if (null == _rootCommand) throw new InvalidOperationException("missing DI setup");

        var workingDirectory = Path.Join(Path.GetTempPath(), Path.GetRandomFileName());

        if (!Directory.Exists(workingDirectory)) Directory.CreateDirectory(workingDirectory);

        var process = Process.Start("C:\\Program Files\\Microsoft VS Code\\Code.exe", "-n " + workingDirectory);

        foreach (var command in commands)
            await RunTest(_rootCommand, command.Replace("#WORKING_DIRECTORY#", workingDirectory));
        
        process.Kill();
        Directory.Delete(workingDirectory, true);
    }

    private static async Task RunTest(RootCommand rootCommand, string args)
    {
        var argsParsed = rootCommand.Parse(args);

        Assert.IsNotNull(argsParsed, args);
        Assert.AreEqual(0, argsParsed.Errors.Count, args, argsParsed.Errors);
        Assert.IsNotNull(argsParsed.CommandResult);
        var invokeResult = await rootCommand.InvokeAsync(args);
        Assert.AreEqual(0, invokeResult, args);
    }

    #endregion
}