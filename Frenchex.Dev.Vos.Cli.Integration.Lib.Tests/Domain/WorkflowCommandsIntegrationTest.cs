using System;
using System.Collections.Generic;
using System.CommandLine;
using System.IO;
using System.Threading.Tasks;
using Frenchex.Dev.Dotnet.Cli.Integration.Lib.Domain;
using Frenchex.Dev.Vos.Cli.Integration.Lib.DependencyInjection;
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

        _di = services.BuildServiceProvider();
        _scope = _di.CreateAsyncScope();
        _scopedDi = _scope?.ServiceProvider;

        _rootCommand = new RootCommand();

        var integration = _scopedDi?.GetRequiredService<IIntegration>();
        integration?.Integrate(_rootCommand);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _scope?.Dispose();
    }

    #endregion

    #region Privates

    private RootCommand? _rootCommand;
    private IServiceProvider? _di;
    private AsyncServiceScope? _scope;
    private IServiceProvider? _scopedDi;

    #endregion

    #region Test_Complete_Workflow_Integration

    public static IEnumerable<object[]> Test_Data()
    {
        var timeOutOpt = "--timeout-ms " + TimeSpan.FromMinutes(10).TotalMilliseconds;
        const string workingDirOpt = "--working-directory #WORKING_DIRECTORY#";

        yield return new object[]
        {
            new[]
            {
                $"init {timeOutOpt} {workingDirOpt}",
                $"define machine-type add foo generic/alpine38 4 128 Debian_64 10.9.0 --enabled {timeOutOpt} {workingDirOpt}",
                $"define machine add foo foo 4 --enabled {timeOutOpt} {workingDirOpt}",
                $"name foo-[2-*] {timeOutOpt} {workingDirOpt}",
                $"status foo-[2-*] {timeOutOpt} {workingDirOpt}",
                $"up foo-0 {timeOutOpt} {workingDirOpt}",
                $"up foo-[2-*] {timeOutOpt} {workingDirOpt}",
                $"status foo-[2-*] {timeOutOpt} {workingDirOpt}",
                $"halt foo-[2-*] {timeOutOpt} {workingDirOpt}",
                $"destroy foo-2 --force {timeOutOpt} {workingDirOpt}",
                $"destroy --force {timeOutOpt} {workingDirOpt}"
            }
        };

        yield return new object[]
        {
            new[]
            {
                $"init {timeOutOpt} {workingDirOpt}",
                $"define machine-type add foo generic/alpine38 4 128 Debian_64 10.9.0 --enabled {timeOutOpt} {workingDirOpt}",
                $"define machine-type add bar generic/alpine38 4 128 Debian_64 10.9.0 --enabled {timeOutOpt} {workingDirOpt}",
                $"define machine add foo foo 4 --enabled {timeOutOpt} {workingDirOpt}",
                $"define machine add bar bar 4 --enabled {timeOutOpt} {workingDirOpt}",
                $"name bar-0 foo-[2-*] {timeOutOpt} {workingDirOpt}",
                $"status bar-* foo-[2-*] {timeOutOpt} {workingDirOpt}",
                $"up foo-0 {timeOutOpt} {workingDirOpt}",
                $"up foo-[2-*] {timeOutOpt} {workingDirOpt}",
                $"status bar-* foo-[2-*] {timeOutOpt} {workingDirOpt}",
                $"halt bar-* foo-[2-*] {timeOutOpt} {workingDirOpt}",
                $"destroy foo-2 --force {timeOutOpt} {workingDirOpt}",
                $"destroy --force {timeOutOpt} {workingDirOpt}"
            }
        };
    }


    [TestMethod]
    [DynamicData(nameof(Test_Data), DynamicDataSourceType.Method)]
    public void Test_Complete_Workflow_Integration_Args(string[] commands)
    {
        if (null == _rootCommand)
            throw new InvalidOperationException("missing DI setup");

        var workingDirectory = Path.Join(Path.GetTempPath(), Path.GetRandomFileName());

        foreach (var command in commands)
        {
            var realArgs = $"vos {command}";
            var argsParsed = _rootCommand.Parse(realArgs);

            Assert.IsNotNull(argsParsed, realArgs);
            Assert.AreEqual(0, argsParsed.Errors.Count, realArgs, argsParsed.Errors);
            Assert.IsNotNull(argsParsed.CommandResult);
        }
    }

    [TestMethod]
    [DynamicData(nameof(Test_Data), DynamicDataSourceType.Method)]
    [TestCategory("need-vagrant")]
    public async Task Test_Complete_Workflow_Integration(string[] commands)
    {
        if (null == _rootCommand)
            throw new InvalidOperationException("missing DI setup");

        var workingDirectory = Path.Join(Path.GetTempPath(), Path.GetRandomFileName());

        if (!Directory.Exists(workingDirectory))
            Directory.CreateDirectory(workingDirectory);

        // var process = Process.Start("C:\\Program Files\\Microsoft VS Code\\Code.exe", "-n " + workingDirectory);

        foreach (var command in commands)
            await RunTest(_rootCommand, command.Replace("#WORKING_DIRECTORY#", workingDirectory));

        //process.Kill();

        Directory.Delete(workingDirectory, true);
    }

    private static async Task RunTest(RootCommand rootCommand, string args)
    {
        var realArgs = $"vos {args}";
        var argsParsed = rootCommand.Parse(realArgs);

        Assert.IsNotNull(argsParsed, realArgs);
        Assert.AreEqual(0, argsParsed.Errors.Count, realArgs, argsParsed.Errors);
        Assert.IsNotNull(argsParsed.CommandResult);

        var invokeResult = await rootCommand.InvokeAsync(realArgs);
        Assert.AreEqual(0, invokeResult, realArgs);
    }

    #endregion
}