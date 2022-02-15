﻿using System;
using System.Collections.Generic;
using System.CommandLine;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Frenchex.Dev.Dotnet.Cli.Integration.Lib.Domain;
using Frenchex.Dev.Dotnet.UnitTesting.Lib.Domain;
using Frenchex.Dev.Vos.Cli.Integration.Lib.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
#pragma warning disable CS8602
#pragma warning disable CS1998

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Tests.Domain;

[TestClass]
public class IntegrationWorkflowUnitTest
{
    private const string WorkingDirectoryMarkholder = "#WORKING_DIRECTORY#";
    private UnitTest? _unitTest;

    [TestInitialize]
    public void Setup()
    {
        _unitTest = new UnitTest(
            builder =>
            {
                builder
                    .AddInMemoryCollection(new List<KeyValuePair<string, string>>
                    {
                        new("MY_KEY", "MY_VALUE")
                    });
            },
            (services, configurationRoot) =>
            {
                // very same class used by Program.cs
                ServicesConfiguration.ConfigureServices(services);

                // DI will be used as receptable of integrated commands
                services.AddScoped<SubjectUnderTest>();
            },
            (services, configurationRoot) =>
            {
                // overload your services to mock them
                var mockedConsole = new Mock<IConsole>();
                services.AddSingleton(provider => mockedConsole.Object);

                // we use DI to hold our mock
                services.AddSingleton(mockedConsole);
            }
        );
    }

    [TestMethod]
    [DynamicData(nameof(Test_Data), DynamicDataSourceType.Method)]
    [TestCategory("need-vagrant")]
    public async Task Test(string testCaseName, InputCommand[] commands)
    {
        var workingDirectory = Path.Join(Path.GetTempPath(), Path.GetRandomFileName());

        await _unitTest
            .OpenVsCode(workingDirectory)
            .RunAsync(
                async (provider, configurationRoot) =>
                {
                    var rootCommand = provider.GetRequiredService<SubjectUnderTest>().RootCommand;
                    var integration = provider.GetRequiredService<IIntegration>();
                    integration.Integrate(rootCommand);
                },
                async (provider, configurationRoot) =>
                {
                    var sut = provider.GetRequiredService<SubjectUnderTest>().RootCommand;

                    if (!Directory.Exists(workingDirectory))
                        Directory.CreateDirectory(workingDirectory);

                    foreach (var command in commands)
                    {
                        var vosCommand = $"vos  {command.Command}"
                            .Replace(WorkingDirectoryMarkholder, workingDirectory);

                        var parsed = sut.Parse(vosCommand);

                        Assert.AreEqual(0, parsed.Errors.Count, parsed.Errors.SelectMany(x => x.Message).ToString());

                        var result = await sut.InvokeAsync(vosCommand);

                        Assert.IsNotNull(result, vosCommand);
                    }

                    Directory.Delete(workingDirectory, true);
                },
                async (provider, configurationRoot) =>
                {
                    // it is useless here
                    // because we have not yet decomposed enough our tests
                    // in the executeFunc, we are asserting parsing and execution
                }
            );
    }

    private static IEnumerable<object[]> Test_Data()
    {
        var timeOutOpt = "--timeout-ms " + TimeSpan.FromMinutes(10).TotalMilliseconds;
        const string workingDirOpt = $"--working-directory {WorkingDirectoryMarkholder}";

        yield return new object[]
        {
            "Test case 1",
            new InputCommand[]
            {
                new("init", $"init {timeOutOpt} {workingDirOpt}"),
                new("d.m.t 1",
                    $"define machine-type add foo generic/alpine38 4 128 Debian_64 10.9.0 --enabled {timeOutOpt} {workingDirOpt}"),
                new("d.m.t 2",
                    $"define machine-type add bar generic/alpine38 4 128 Debian_64 10.9.0 --enabled {timeOutOpt} {workingDirOpt}"),
                new("d.m 1", $"define machine add foo foo 4 --enabled {timeOutOpt} {workingDirOpt}"),
                new("d.m 2", $"define machine add bar bar 4 --enabled {timeOutOpt} {workingDirOpt}"),
                new("name", $"name bar-0 foo-[2-*] {timeOutOpt} {workingDirOpt}"),
                new("status", $"status bar-* foo-[2-*] {timeOutOpt} {workingDirOpt}"),
                new("up foo0", $"up foo-0 {timeOutOpt} {workingDirOpt}"),
                new("up foo2-*", $"up foo-[2-*] {timeOutOpt} {workingDirOpt}"),
                new("status bar* foo2-*", $"status bar-* foo-[2-*] {timeOutOpt} {workingDirOpt}"),
                new("halt bar-* foo2-*", $"halt bar-* foo-[2-*] {timeOutOpt} {workingDirOpt}"),
                new("destroy foo2", $"destroy foo-2 --force {timeOutOpt} {workingDirOpt}"),
                new("destroy all", $"destroy --force {timeOutOpt} {workingDirOpt}")
            }
        };

        yield return new object[]
        {
            "Test case 2",
            new InputCommand[]
            {
                new("init", $"init {timeOutOpt} {workingDirOpt}"),
                new("d.m.t 1",
                    $"define machine-type add foo generic/alpine38 4 128 Debian_64 10.9.0 --enabled {timeOutOpt} {workingDirOpt}"),
                new("d.m.t 2",
                    $"define machine-type add bar generic/alpine38 4 128 Debian_64 10.9.0 --enabled {timeOutOpt} {workingDirOpt}"),
                new("d.m 1", $"define machine add foo foo 4 --enabled {timeOutOpt} {workingDirOpt}"),
                new("d.m 2", $"define machine add bar bar 4 --enabled {timeOutOpt} {workingDirOpt}"),
                new("name", $"name bar-0 foo-[2-*] {timeOutOpt} {workingDirOpt}"),
                new("status", $"status bar-* foo-[2-*] {timeOutOpt} {workingDirOpt}"),
                new("up foo0", $"up foo-0 {timeOutOpt} {workingDirOpt}"),
                new("up foo2-*", $"up foo-[2-*] {timeOutOpt} {workingDirOpt}"),
                new("status bar* foo2-*", $"status bar-* foo-[2-*] {timeOutOpt} {workingDirOpt}"),
                new("halt bar-* foo2-*", $"halt bar-* foo-[2-*] {timeOutOpt} {workingDirOpt}"),
                new("destroy foo2", $"destroy foo-2 --force {timeOutOpt} {workingDirOpt}"),
                new("destroy all", $"destroy --force {timeOutOpt} {workingDirOpt}")
            }
        };
    }
}

public class SubjectUnderTest
{
    public SubjectUnderTest()
    {
        RootCommand = new RootCommand();
    }

    public RootCommand RootCommand { get; }
}

public class InputCommand
{
    public InputCommand(
        string name,
        string command
    )
    {
        Name = name;
        Command = command;
    }

    public string Name { get; }
    public string Command { get; }
}