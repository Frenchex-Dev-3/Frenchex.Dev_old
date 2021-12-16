﻿using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Destroy;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Halt;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Init;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Ssh;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.SshConfig;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Status;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Up;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Frenchex.Dev.Vagrant.Lib.Tests.Domain.Commands
{
    [TestClass]
    public class CompleteWorkflowTests
    {
        #region Static Privates
        private static IServiceProvider? _di;

        private static IInitCommandRequestBuilderFactory? _initCommandRequestBuilderFactory;
        private static IStatusCommandRequestBuilderFactory? _statusCommandRequestBuilderFactory;
        private static IUpCommandRequestBuilderFactory? _upCommandRequestBuilderFactory;
        private static ISshConfigCommandRequestBuilderFactory? _sshConfigCommandRequestBuilderFactory;
        private static ISshCommandRequestBuilderFactory? _sshCommandRequestBuilderFactory;
        private static IHaltCommandRequestBuilderFactory? _haltCommandRequestBuilderFactory;
        private static IDestroyCommandRequestBuilderFactory? _destroyCommandRequestBuilderFactory;
        #endregion

        #region Static Setup
        protected static IServiceCollection SetupServices()
        {
            var services = new ServiceCollection();

            DependencyInjection
                .ServicesConfiguration
                .ConfigureServices(services)
                ;

            return services;
        }

        [ClassInitialize]
        public static void SetupClass(TestContext testContext)
        {
            if (testContext is null)
            {
                throw new ArgumentNullException(nameof(testContext));
            }

            _di = SetupServices().BuildServiceProvider();

            _initCommandRequestBuilderFactory = _di.GetRequiredService<IInitCommandRequestBuilderFactory>();
            _statusCommandRequestBuilderFactory = _di.GetRequiredService<IStatusCommandRequestBuilderFactory>();
            _upCommandRequestBuilderFactory = _di.GetRequiredService<IUpCommandRequestBuilderFactory>();
            _sshConfigCommandRequestBuilderFactory = _di.GetRequiredService<ISshConfigCommandRequestBuilderFactory>();
            _sshCommandRequestBuilderFactory = _di.GetRequiredService<ISshCommandRequestBuilderFactory>();
            _haltCommandRequestBuilderFactory = _di.GetRequiredService<IHaltCommandRequestBuilderFactory>();
            _destroyCommandRequestBuilderFactory = _di.GetRequiredService<IDestroyCommandRequestBuilderFactory>();
        }
        #endregion

        #region Privates
        IInitCommand? _realInitCommand;
        IUpCommand? _realUpCommand;
        IStatusCommand? _realStatusCommand;
        ISshCommand? _realSshCommand;
        ISshConfigCommand? _realSshConfigCommand;
        IHaltCommand? _realHaltCommand;
        IDestroyCommand? _realDestroyCommand;
        #endregion

        #region Setup
        [TestInitialize]
        public void Setup()
        {
            if (null == _di)
            {
                throw new InvalidOperationException("No DI setup");
            }

            _realInitCommand = _di.GetRequiredService<IInitCommand>();
            _realUpCommand = _di.GetRequiredService<IUpCommand>();
            _realStatusCommand = _di.GetRequiredService<IStatusCommand>();
            _realSshCommand = _di.GetRequiredService<ISshCommand>();
            _realSshConfigCommand = _di.GetRequiredService<ISshConfigCommand>();
            _realHaltCommand = _di.GetRequiredService<IHaltCommand>();
            _realDestroyCommand = _di.GetRequiredService<IDestroyCommand>();
        }
        #endregion

        #region Test_Complete_Workflow

        public static IEnumerable<object[]> Test_Data()
        {
            var tempDir = Path.Join(Path.GetTempPath(), Path.GetRandomFileName());

            if (null == _initCommandRequestBuilderFactory
                || null == _upCommandRequestBuilderFactory
                || null == _statusCommandRequestBuilderFactory
                || null == _sshConfigCommandRequestBuilderFactory
                || null == _sshCommandRequestBuilderFactory
                || null == _haltCommandRequestBuilderFactory
                || null == _destroyCommandRequestBuilderFactory
                )
            {
                throw new InvalidOperationException("Command RequestFactory");
            }

            yield return new object[]
            {
                _initCommandRequestBuilderFactory.Factory()
                    .BaseBuilder
                       .UsingTimeoutMiliseconds(1000 * 1000)
                       .UsingWorkingDirectory(tempDir)
                       .Parent<InitCommandRequestBuilder>()
                    .UsingBoxName("debian/buster64")
                    .Build()
                ,
               _upCommandRequestBuilderFactory.Factory()
                    .BaseBuilder
                        .UsingTimeoutMiliseconds(1000 * 1000)
                        .UsingWorkingDirectory(tempDir)
                        .Parent<UpCommandRequestBuilder>()
                    .Build()
                ,
               _statusCommandRequestBuilderFactory.Factory()
                .BaseBuilder
                        .UsingTimeoutMiliseconds(1000 * 1000)
                        .UsingWorkingDirectory(tempDir)
                        .Parent<StatusCommandRequestBuilder>()
                    .Build(),
                _sshConfigCommandRequestBuilderFactory.Factory()
                    .BaseBuilder
                        .UsingTimeoutMiliseconds(1000 * 1000)
                        .UsingWorkingDirectory(tempDir)
                        .Parent<SshConfigCommandRequestBuilder>()
                    .UsingName("default")
                    .Build()
                ,
                _sshCommandRequestBuilderFactory.Factory()
                    .BaseBuilder
                        .UsingTimeoutMiliseconds(1000 * 1000)
                        .UsingWorkingDirectory(tempDir)
                        .Parent<SshCommandRequestBuilder>()
                    .UsingCommand("echo foo")
                    .Build()
                ,
                _haltCommandRequestBuilderFactory.Factory()
                    .BaseBuilder
                        .UsingTimeoutMiliseconds(1000 * 1000)
                        .UsingWorkingDirectory(tempDir)
                        .Parent<HaltCommandRequestBuilder>()
                    .Build()
                ,
                _destroyCommandRequestBuilderFactory.Factory()
                    .BaseBuilder
                        .UsingTimeoutMiliseconds(1000 * 1000)
                        .UsingWorkingDirectory(tempDir)
                        .Parent<DestroyCommandRequestBuilder>()
                    .WithForce(true)
                    .Build()
            };
        }

        [TestMethod]
        [DynamicData(nameof(Test_Data), DynamicDataSourceType.Method)]
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
            if (null == _realInitCommand
               || null == _realStatusCommand
               || null == _realUpCommand
               || null == _realSshCommand
               || null == _realSshConfigCommand
               || null == _realHaltCommand
               || null == _realDestroyCommand
               )
            {
                throw new InvalidOperationException("Command RequestFactory");
            }

            await TestInner(_realInitCommand.StartProcess(initRequest), true);
            await TestInner(_realStatusCommand.StartProcess(statusRequest));
            await TestInner(_realUpCommand.StartProcess(upRequest));
            await TestInner(_realSshConfigCommand.StartProcess(sshConfigCommandRequest));
            await TestInner(_realSshCommand.StartProcess(sshCommandRequest));
            await TestInner(_realHaltCommand.StartProcess(haltRequest));
            await TestInner(_realDestroyCommand.StartProcess(destroyRequest));

            // generic asserts
            Assert.IsTrue(Directory.Exists(initRequest.Base.WorkingDirectory));
            Assert.IsTrue(File.Exists(Path.Join(initRequest.Base.WorkingDirectory, "Vagrantfile")));

            //clean
            Directory.Delete(initRequest.Base.WorkingDirectory, true);
            Assert.IsFalse(Directory.Exists(initRequest.Base.WorkingDirectory));
        }

        private static async Task TestInner(IRootCommandResponse response, bool outputCanBeEmptyButNotNull = false)
        {
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.ProcessExecutionResult);
            Assert.IsNotNull(response.ProcessExecutionResult.WaitForCompleteExit);
            Assert.IsNotNull(response.ProcessExecutionResult.OutputStream);

            await response.ProcessExecutionResult.WaitForCompleteExit;

            response.ProcessExecutionResult.OutputStream.Position = 0;
            var outputReader = new StreamReader(response.ProcessExecutionResult.OutputStream);
            var output = await outputReader.ReadToEndAsync();

            Assert.AreEqual(0, response.ProcessExecutionResult.ExitCode);

            if (outputCanBeEmptyButNotNull)
            {
                Assert.IsNotNull(output);
            }
            else
            {
                Assert.IsTrue(!string.IsNullOrEmpty(output));
            }
        }

        #endregion
    }
}