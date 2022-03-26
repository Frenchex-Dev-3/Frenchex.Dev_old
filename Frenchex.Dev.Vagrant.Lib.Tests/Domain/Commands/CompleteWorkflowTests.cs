using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Frenchex.Dev.Dotnet.UnitTesting.Lib.Domain;
using Frenchex.Dev.Vagrant.Lib.DependencyInjection;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Destroy;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Halt;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Init;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Ssh;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.SshConfig;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Status;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Up;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

[assembly: Parallelize(Workers = 2, Scope = ExecutionScope.MethodLevel)]

namespace Frenchex.Dev.Vagrant.Lib.Tests.Domain.Commands;

[TestClass]
public class CompleteWorkflowTests
{
    private static UnitTest? _unitTest;

    static CompleteWorkflowTests()
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

                // DI will be used as receptacle of integrated commands
                // by means of SubjectUnderTest
                services.AddScoped<IConfiguration>(_ => configurationRoot);
            },
            (services, _) =>
            {
                // overload your services to mock them
                var mockedConsole = new Mock<IConsole>();
                services.AddSingleton(_ => mockedConsole.Object);

                // we use DI to hold our mock
                services.AddSingleton(mockedConsole);
            }
        );

        _unitTest.Build();
    }

    public static IEnumerable<object[]> Test_Data()
    {
        var tempDir = Path.Join(Path.GetTempPath(), Path.GetRandomFileName());

        Debug.Assert(_unitTest != null, nameof(_unitTest) + " != null");
        Debug.Assert(_unitTest.Provider != null, "_unitTest.Provider != null");

        yield return new object[]
        {
            _unitTest.Provider
                .GetRequiredService<IInitCommandRequestBuilderFactory>().Factory()
                .BaseBuilder
                .UsingTimeoutMiliseconds((int) TimeSpan.FromMinutes(1).TotalMilliseconds)
                .UsingWorkingDirectory(tempDir)
                .Parent<IInitCommandRequestBuilder>()
                .UsingBoxName("generic/alpine38")
                .Build(),
            _unitTest.Provider
                .GetRequiredService<IUpCommandRequestBuilderFactory>().Factory()
                .BaseBuilder
                .UsingTimeoutMiliseconds((int) TimeSpan.FromMinutes(10).TotalMilliseconds)
                .UsingWorkingDirectory(tempDir)
                .Parent<IUpCommandRequestBuilder>()
                .Build(),
            _unitTest.Provider
                .GetRequiredService<IStatusCommandRequestBuilderFactory>().Factory()
                .BaseBuilder
                .UsingTimeoutMiliseconds((int) TimeSpan.FromMinutes(1).TotalMilliseconds)
                .UsingWorkingDirectory(tempDir)
                .Parent<IStatusCommandRequestBuilder>()
                .Build(),
            _unitTest.Provider
                .GetRequiredService<ISshConfigCommandRequestBuilderFactory>().Factory()
                .BaseBuilder
                .UsingTimeoutMiliseconds((int) TimeSpan.FromMinutes(1).TotalMilliseconds)
                .UsingWorkingDirectory(tempDir)
                .Parent<ISshConfigCommandRequestBuilder>()
                .UsingName("default")
                .Build(),
            _unitTest.Provider
                .GetRequiredService<ISshCommandRequestBuilderFactory>().Factory()
                .BaseBuilder
                .UsingTimeoutMiliseconds((int) TimeSpan.FromMinutes(1).TotalMilliseconds)
                .UsingWorkingDirectory(tempDir)
                .Parent<ISshCommandRequestBuilder>()
                .UsingCommand("echo foo")
                .UsingName("default")
                .Build(),
            _unitTest.Provider
                .GetRequiredService<IHaltCommandRequestBuilderFactory>().Factory()
                .BaseBuilder
                .UsingTimeoutMiliseconds((int) TimeSpan.FromMinutes(3).TotalMilliseconds)
                .UsingWorkingDirectory(tempDir)
                .Parent<IHaltCommandRequestBuilder>()
                .Build(),
            _unitTest.Provider
                .GetRequiredService<IDestroyCommandRequestBuilderFactory>().Factory()
                .BaseBuilder
                .UsingTimeoutMiliseconds((int) TimeSpan.FromMinutes(3).TotalMilliseconds)
                .UsingWorkingDirectory(tempDir)
                .Parent<IDestroyCommandRequestBuilder>()
                .WithForce(true)
                .Build()
        };
    }

    [TestMethod]
    [DynamicData(nameof(Test_Data), DynamicDataSourceType.Method)]
    [TestCategory("need-vagrant")]
    public async Task Test_Complete_Workflow(
        IInitCommandRequest initRequest,
        IUpCommandRequest upRequest,
        IStatusCommandRequest statusRequest,
        ISshConfigCommandRequest sshConfigCommandRequest,
        ISshCommandRequest sshCommandRequest,
        IHaltCommandRequest haltRequest,
        IDestroyCommandRequest destroyRequest
    )
    {
        Debug.Assert(_unitTest != null, nameof(_unitTest) + " != null");
        Debug.Assert(_unitTest.Provider != null, "_unitTest.Provider != null");

        var services = _unitTest.Provider;

        await TestInner("init", services.GetRequiredService<IInitCommand>().StartProcess(initRequest), true);
        await TestInner("status", services.GetRequiredService<IStatusCommand>().StartProcess(statusRequest));
        await TestInner("up", services.GetRequiredService<IUpCommand>().StartProcess(upRequest));
        await TestInner("ssh-config",
            services.GetRequiredService<ISshConfigCommand>().StartProcess(sshConfigCommandRequest));
        await TestInner("ssh", services.GetRequiredService<ISshCommand>().StartProcess(sshCommandRequest));
        await TestInner("halt", services.GetRequiredService<IHaltCommand>().StartProcess(haltRequest));
        await TestInner("destroy", services.GetRequiredService<IDestroyCommand>().StartProcess(destroyRequest));

        // generic asserts
        Assert.IsTrue(Directory.Exists(initRequest.Base.WorkingDirectory));
        Assert.IsTrue(File.Exists(Path.Join(initRequest.Base.WorkingDirectory, "Vagrantfile")));

        //clean
        Directory.Delete(initRequest.Base.WorkingDirectory, true);
        Assert.IsFalse(Directory.Exists(initRequest.Base.WorkingDirectory));
    }

    private static async Task TestInner(string debug, IRootCommandResponse response,
        bool outputCanBeEmptyButNotNull = false)
    {
        Assert.IsNotNull(response, $"{debug} response is not null");
        Assert.IsNotNull(response.ProcessExecutionResult, $"{debug} response.PER is not null");
        Assert.IsNotNull(response.ProcessExecutionResult.WaitForCompleteExit, $"{debug} response.WaitForComplexeExit");
        Assert.IsNotNull(response.ProcessExecutionResult.OutputStream, $"{debug} response outputstream");

        await response.ProcessExecutionResult.WaitForCompleteExit;

        response.ProcessExecutionResult.OutputStream.Position = 0;
        var outputReader = new StreamReader(response.ProcessExecutionResult.OutputStream);
        var output = await outputReader.ReadToEndAsync();

        Assert.AreEqual(0, response.ProcessExecutionResult.ExitCode, $"{debug} exit code is zero");

        if (outputCanBeEmptyButNotNull)
            Assert.IsNotNull(output, $"{debug} output can be empty but not null");
        else
            Assert.IsTrue(!string.IsNullOrEmpty(output), $"{debug} output is neither empty nor null");
    }
}