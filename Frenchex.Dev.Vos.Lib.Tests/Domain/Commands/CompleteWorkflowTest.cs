using Frenchex.Dev.Vagrant.Lib.Domain;
using Frenchex.Dev.Vos.Lib.Domain.Bases;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Define.Machine.Add;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Define.MachineType.Add;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Destroy;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Halt;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Init;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Ssh;
using Frenchex.Dev.Vos.Lib.Domain.Commands.SshConfig;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Status;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Up;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Name;

namespace Frenchex.Dev.Vos.Lib.Tests.Domain.Commands
{
    [TestClass]
    public class CompleteWorkflowTests
    {
        static IServiceProvider? _di;

        // factories used to build requests in dynamic method ::Test_Data
        static IInitCommandRequestBuilderFactory? _initCommandRequestBuilderFactory;
        static IMachineTypeDefinitionBuilderFactory? _machineTypeDefinitionBuilderFactory;
        static IMachineDefinitionBuilderFactory? _machineDefinitionBuilderFactory;
        static IDefineMachineTypeAddCommandRequestBuilderFactory? _defineMachineTypeAddCommandRequestBuilderFactory;
        static IDefineMachineAddCommandRequestBuilderFactory? _defineMachineAddCommandRequestBuilderFactory;
        static IStatusCommandRequestBuilderFactory? _statusCommandRequestBuilderFactory;
        static IUpCommandRequestBuilderFactory? _upCommandRequestBuilderFactory;
        static ISshConfigCommandRequestBuilderFactory? _sshConfigCommandRequestBuilderFactory;
        static ISshCommandRequestBuilderFactory? _sshCommandRequestBuilderFactory;
        static IHaltCommandRequestBuilderFactory? _haltCommandRequestBuilderFactory;
        static IDestroyCommandRequestBuilderFactory? _destroyCommandRequestBuilderFactory;

        // flow commands
        static IInitCommand? _initCommand;
        static IDefineMachineTypeAddCommand? _defineMachineTypeAddCommand;
        static IDefineMachineAddCommand? _defineMachineAddCommand;
        static IStatusCommand? _statusCommand;
        static IUpCommand? _upCommand;
        static ISshCommand? _sshCommand;
        static ISshConfigCommand? _sshConfigCommand;
        static IHaltCommand? _haltCommand;
        static IDestroyCommand? _destroyCommand;

        // helper command
        private static INameCommand? _nameCommand;
        private static INameCommandRequestBuilderFactory? _nameCommandRequestBuilderFactory;

        protected static IServiceCollection SetupServices()
        {
            var services = new ServiceCollection();

            DependencyInjection
                .ServicesConfiguration
                .ConfigureServices(services);

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
            _machineTypeDefinitionBuilderFactory = _di.GetRequiredService<IMachineTypeDefinitionBuilderFactory>();
            _machineDefinitionBuilderFactory = _di.GetRequiredService<IMachineDefinitionBuilderFactory>();
            _defineMachineTypeAddCommandRequestBuilderFactory = _di.GetRequiredService<IDefineMachineTypeAddCommandRequestBuilderFactory>();
            _defineMachineAddCommandRequestBuilderFactory = _di.GetRequiredService<IDefineMachineAddCommandRequestBuilderFactory>();
            _statusCommandRequestBuilderFactory = _di.GetRequiredService<IStatusCommandRequestBuilderFactory>();
            _upCommandRequestBuilderFactory = _di.GetRequiredService<IUpCommandRequestBuilderFactory>();
            _sshConfigCommandRequestBuilderFactory = _di.GetRequiredService<ISshConfigCommandRequestBuilderFactory>();
            _sshCommandRequestBuilderFactory = _di.GetRequiredService<ISshCommandRequestBuilderFactory>();
            _haltCommandRequestBuilderFactory = _di.GetRequiredService<IHaltCommandRequestBuilderFactory>();
            _destroyCommandRequestBuilderFactory = _di.GetRequiredService<IDestroyCommandRequestBuilderFactory>();

            _initCommand = _di.GetRequiredService<IInitCommand>();
            _defineMachineTypeAddCommand = _di.GetRequiredService<IDefineMachineTypeAddCommand>();
            _defineMachineAddCommand = _di.GetRequiredService<IDefineMachineAddCommand>();
            _upCommand = _di.GetRequiredService<IUpCommand>();
            _statusCommand = _di.GetRequiredService<IStatusCommand>();
            _sshCommand = _di.GetRequiredService<ISshCommand>();
            _sshConfigCommand = _di.GetRequiredService<ISshConfigCommand>();
            _haltCommand = _di.GetRequiredService<IHaltCommand>();
            _destroyCommand = _di.GetRequiredService<IDestroyCommand>();

            _nameCommand = _di.GetRequiredService<INameCommand>();
            _nameCommandRequestBuilderFactory = _di.GetRequiredService<INameCommandRequestBuilderFactory>();
        }

        public static IEnumerable<object[]> Test_Data()
        {
            var tempDir = Path.Join(Path.GetTempPath(), Path.GetRandomFileName());
            yield return new object[]
            {
                BuildInitCommandRequest(tempDir),
                BuildDefineMachineTypeAddCommandRequestsList(tempDir),
                BuildDefineMachineAddCommandRequestsList(tempDir),
                BuildStatusBeforeUpCommandRequestsList(tempDir),
                BuildUpCommandRequestsList(tempDir),
                BuildStatusAfterUpCommandRequestsList(tempDir),
                BuildSshConfigCommandRequestsList(tempDir),
                BuildSshCommandRequestsList(tempDir),
                BuildHaltCommandRequestsList(tempDir),
                BuildDestroyCommandRequestsList(tempDir),
                true,
                true,
                false
            };
        }

        private static List<IStatusCommandRequest> BuildStatusAfterUpCommandRequestsList(string tempDir)
        {
            if (null == _statusCommandRequestBuilderFactory
            )
            {
                throw new InvalidOperationException("null");
            }

            return new List<IStatusCommandRequest>()
            {
                _statusCommandRequestBuilderFactory.Factory()
                    .BaseBuilder
                        .UsingWorkingDirectory(tempDir)
                        .UsingTimeoutMiliseconds(1000 * 100)
                        .Parent<IStatusCommandRequestBuilder>()
                    .WithNames(new string[]{ "foo-0", "foo-1" })
                .Build(),
                _statusCommandRequestBuilderFactory.Factory()
                    .BaseBuilder
                        .UsingWorkingDirectory(tempDir)
                        .UsingTimeoutMiliseconds(1000 * 100)
                        .Parent<IStatusCommandRequestBuilder>()
                    .WithNames(new string[]{ "bar-1" })
                .Build(),
                _statusCommandRequestBuilderFactory.Factory()
                    .BaseBuilder
                        .UsingWorkingDirectory(tempDir)
                        .UsingTimeoutMiliseconds(1000 * 100)
                        .Parent<IStatusCommandRequestBuilder>()
                    .WithNames(new string[]{ "bar-0" })
                .Build()
            };
        }

        private static List<IStatusCommandRequest> BuildStatusBeforeUpCommandRequestsList(string tempDir)
        {
            if (null == _statusCommandRequestBuilderFactory
)
            {
                throw new InvalidOperationException("null");
            }

            return new List<IStatusCommandRequest>()
            {
                _statusCommandRequestBuilderFactory.Factory()
                    .BaseBuilder
                        .UsingWorkingDirectory(tempDir)
                        .UsingTimeoutMiliseconds(1000 * 100)
                        .Parent<IStatusCommandRequestBuilder>()
                .Build()
            };
        }

        private static List<IDestroyCommandRequest> BuildDestroyCommandRequestsList(string tempDir)
        {
            if (null == _destroyCommandRequestBuilderFactory
)
            {
                throw new InvalidOperationException("null");
            }

            return new List<IDestroyCommandRequest>()
            {
                    _destroyCommandRequestBuilderFactory.Factory()
                    .BaseBuilder
                        .UsingWorkingDirectory(tempDir)
                        .UsingTimeoutMiliseconds(1000*100)
                        .Parent<IDestroyCommandRequestBuilder>()
                    .WithForce(true)
                    .UsingName("foo-0")
                    .Build(),
                    _destroyCommandRequestBuilderFactory.Factory()
                    .BaseBuilder
                        .UsingWorkingDirectory(tempDir)
                        .UsingTimeoutMiliseconds(1000*100)
                        .Parent<IDestroyCommandRequestBuilder>()
                    .WithForce(true)
                    .UsingName("foo-1")
                    .Build(),
                    _destroyCommandRequestBuilderFactory.Factory()
                    .BaseBuilder
                        .UsingWorkingDirectory(tempDir)
                        .UsingTimeoutMiliseconds(1000*100)
                        .Parent<IDestroyCommandRequestBuilder>()
                    .WithForce(true)
                    .Build()
                };
        }

        private static List<IHaltCommandRequest> BuildHaltCommandRequestsList(string tempDir)
        {
            if (null == _haltCommandRequestBuilderFactory
)
            {
                throw new InvalidOperationException("null");
            }

            return new List<IHaltCommandRequest>()
                {
                     _haltCommandRequestBuilderFactory.Factory()
                    .BaseBuilder
                        .UsingWorkingDirectory(tempDir)
                        .UsingTimeoutMiliseconds(1000*100)
                        .Parent<IHaltCommandRequestBuilder>()
                    .UsingNames(new string[]{"foo-0", "foo-1" })
                    .Build(),
                     _haltCommandRequestBuilderFactory.Factory()
                    .BaseBuilder
                        .UsingWorkingDirectory(tempDir)
                        .UsingTimeoutMiliseconds(1000*100)
                        .Parent<IHaltCommandRequestBuilder>()
                    .UsingNames(new string[]{"bar-0"})
                    .Build(),
                     _haltCommandRequestBuilderFactory.Factory()
                    .BaseBuilder
                        .UsingWorkingDirectory(tempDir)
                        .UsingTimeoutMiliseconds(1000*100)
                        .Parent<IHaltCommandRequestBuilder>()
                    .Build()
                };
        }

        private static List<ISshCommandRequest> BuildSshCommandRequestsList(string tempDir)
        {
            if (null == _sshCommandRequestBuilderFactory
)
            {
                throw new InvalidOperationException("null");
            }

            return new List<ISshCommandRequest>()
                {
                    _sshCommandRequestBuilderFactory.Factory()
                    .BaseBuilder
                        .UsingWorkingDirectory(tempDir)
                        .UsingTimeoutMiliseconds(1000*100)
                        .Parent<ISshCommandRequestBuilder>()
                    .UsingName("foo-1")
                    .UsingCommand("echo foo")
                    .Build(),
                       _sshCommandRequestBuilderFactory.Factory()
                    .BaseBuilder
                        .UsingWorkingDirectory(tempDir)
                        .UsingTimeoutMiliseconds(1000*100)
                        .Parent<ISshCommandRequestBuilder>()
                    .UsingName("bar-0")
                    .UsingCommand("echo bar")
                    .Build()
                };
        }

        private static List<ISshConfigCommandRequest> BuildSshConfigCommandRequestsList(string tempDir)
        {
            if (null == _sshConfigCommandRequestBuilderFactory)
            {
                throw new InvalidOperationException("null");
            }

            return new List<ISshConfigCommandRequest>()
                {
                    _sshConfigCommandRequestBuilderFactory.Factory()
                    .BaseBuilder
                        .UsingWorkingDirectory(tempDir)
                        .UsingTimeoutMiliseconds(1000*100)
                        .Parent<ISshConfigCommandRequestBuilder>()
                    .UsingName("foo-0")
                    .Build(),
                     _sshConfigCommandRequestBuilderFactory.Factory()
                    .BaseBuilder
                        .UsingWorkingDirectory(tempDir)
                        .UsingTimeoutMiliseconds(1000*100)
                        .Parent<ISshConfigCommandRequestBuilder>()
                    .UsingName("bar-1")
                    .Build()
                };
        }

        private static List<IUpCommandRequest> BuildUpCommandRequestsList(string tempDir)
        {
            if (null == _upCommandRequestBuilderFactory)
            {
                throw new InvalidOperationException("null");
            }

            return new List<IUpCommandRequest>()
                {
                      _upCommandRequestBuilderFactory.Factory()
                        .BaseBuilder
                            .UsingWorkingDirectory(tempDir)
                            .UsingTimeoutMiliseconds(1000*100)
                            .Parent<IUpCommandRequestBuilder>()
                        .UsingNames(new string[] { "foo-0", "foo-1", "foo-2"})
                        .WithParallel(true)
                        .Build()
                };
        }

        private static List<IDefineMachineAddCommandRequest> BuildDefineMachineAddCommandRequestsList(string tempDir)
        {
            if (null == _defineMachineAddCommandRequestBuilderFactory
                || null == _machineDefinitionBuilderFactory
            )
            {
                throw new InvalidOperationException("null");
            }

            return new List<IDefineMachineAddCommandRequest>()
                {
                    _defineMachineAddCommandRequestBuilderFactory.Factory()
                        .BaseBuilder
                            .UsingWorkingDirectory(tempDir)
                            .UsingTimeoutMiliseconds(1000*100)
                            .Parent<IDefineMachineAddCommandRequestBuilder>()
                        .UsingDefinition(_machineDefinitionBuilderFactory.Factory()
                                .BaseBuilder
                                    .With3DEnabled(true)
                                    .WithBiosLogoImage("")
                                    .WithBox("generic/alpine38")
                                    .WithFiles(new Dictionary<string, FileCopyDefinition>())
                                    .WithGui(false)
                                    .WithOsType(OsTypeEnum.Debian_64)
                                    .WithOsVersion("10.9.1")
                                    .WithPageFusion(false)
                                    .WithProvider(ProviderEnum.virtualbox)
                                    .WithProvisioning(new Dictionary<string, ProvisioningDefinition>())
                                    .WithSharedFolders(new Dictionary<string, SharedFolderDefinition>())
                                    .WithVideoRamInMB(16)
                                    .WithRamInMB(128)
                                    .WithVirtualCpus(2)
                                    .Parent<IMachineDefinitionBuilder>()
                            .WithMachineType("foo")
                            .WithName("foo")
                            .WithNamingPattern("#{name}-#{instance}")
                            .WithInstances(4)
                            .WithIPv4Start(2)
                            .WithIPv4Pattern("10.100.1.#{number}")
                            .IsPrimary(true)
                            .Enabled(true)
                            .WithVirtualCPUs(2)
                            .WithRamInMB(128)
                            .Build()
                        )
                        .Build(),
                    _defineMachineAddCommandRequestBuilderFactory.Factory()
                        .BaseBuilder
                            .UsingWorkingDirectory(tempDir)
                            .UsingTimeoutMiliseconds(1000*100)
                            .Parent<IDefineMachineAddCommandRequestBuilder>()
                        .UsingDefinition(_machineDefinitionBuilderFactory.Factory()
                             .BaseBuilder
                                        .With3DEnabled(true)
                                        .WithBiosLogoImage("")
                                        .WithBox("generic/alpine38")
                                        .WithFiles(new Dictionary<string, FileCopyDefinition>())
                                        .WithGui(false)
                                        .WithOsType(OsTypeEnum.Debian_64)
                                        .WithOsVersion("10.9.1")
                                        .WithPageFusion(false)
                                        .WithProvider(ProviderEnum.virtualbox)
                                        .WithProvisioning(new Dictionary<string, ProvisioningDefinition>())
                                        .WithRamInMB(128)
                                        .WithSharedFolders(new Dictionary<string, SharedFolderDefinition>())
                                        .WithVideoRamInMB(16)
                                        .WithVirtualCpus(2)
                                        .Parent<IMachineDefinitionBuilder>()
                            .WithMachineType("bar")
                            .WithName("bar")
                            .WithNamingPattern("#{name}-#{instance}")
                            .WithInstances(4)
                            .WithIPv4Start(2)
                            .WithIPv4Pattern("10.100.2.#{number}")
                            .IsPrimary(false)
                            .Enabled(true)
                            .WithVirtualCPUs(2)
                            .WithRamInMB(128)
                            .Build()
                        )
                        .Build()
                };
        }

        private static List<IDefineMachineTypeAddCommandRequest> BuildDefineMachineTypeAddCommandRequestsList(string tempDir)
        {
            if (null == _defineMachineTypeAddCommandRequestBuilderFactory
                || null == _machineTypeDefinitionBuilderFactory
            )
            {
                throw new InvalidOperationException("null");
            }


            return new List<IDefineMachineTypeAddCommandRequest>()
                {
                    _defineMachineTypeAddCommandRequestBuilderFactory.Factory()
                        .BaseBuilder
                            .UsingWorkingDirectory(tempDir)
                            .UsingTimeoutMiliseconds(1000*100)
                            .Parent<IDefineMachineTypeAddCommandRequestBuilder>()
                        .UsingDefinition(_machineTypeDefinitionBuilderFactory.Factory()
                            .BaseBuilder
                                .With3DEnabled(true)
                                .WithBiosLogoImage("")
                                .WithBox("generic/alpine38")
                                .WithFiles(new Dictionary<string, FileCopyDefinition>())
                                .WithGui(false)
                                .WithOsType(OsTypeEnum.Debian_64)
                                .WithOsVersion("10.5.0")
                                .WithPageFusion(false)
                                .WithRamInMB(128)
                                .WithVideoRamInMB(16)
                                .WithVirtualCpus(4)
                                .WithFiles(new Dictionary<string, FileCopyDefinition>())
                                .WithProvisioning(new Dictionary<string, ProvisioningDefinition>())
                                .WithSharedFolders(new Dictionary<string, SharedFolderDefinition>())
                                .Parent<IMachineTypeDefinitionBuilder>()
                            .WithName("foo")
                            .Build()
                        )
                        .Build(),
                     _defineMachineTypeAddCommandRequestBuilderFactory.Factory()
                        .BaseBuilder
                            .UsingWorkingDirectory(tempDir)
                            .UsingTimeoutMiliseconds(1000*100)
                            .Parent<IDefineMachineTypeAddCommandRequestBuilder>()
                        .UsingDefinition(_machineTypeDefinitionBuilderFactory.Factory()
                            .BaseBuilder
                                .With3DEnabled(true)
                                .WithBiosLogoImage("")
                                .WithBox("generic/alpine38")
                                .WithFiles(new Dictionary<string, FileCopyDefinition>())
                                .WithGui(false)
                                .WithOsType(OsTypeEnum.Debian_64)
                                .WithOsVersion("10.5.0")
                                .WithPageFusion(false)
                                .WithRamInMB(128)
                                .WithVideoRamInMB(16)
                                .WithVirtualCpus(4)
                                .WithFiles(new Dictionary<string, FileCopyDefinition>())
                                .WithProvisioning(new Dictionary<string, ProvisioningDefinition>())
                                .WithSharedFolders(new Dictionary<string, SharedFolderDefinition>())
                                .Parent<IMachineTypeDefinitionBuilder>()
                            .WithName("bar")
                            .Build()
                        )
                        .Build()
                };
        }

        private static IInitCommandRequest BuildInitCommandRequest(string tempDir)
        {
            if (null == _initCommandRequestBuilderFactory
            )
            {
                throw new InvalidOperationException("null");
            }

            return _initCommandRequestBuilderFactory.Factory()
                                .BaseBuilder
                                    .UsingWorkingDirectory(tempDir)
                                    .UsingTimeoutMiliseconds(1000 * 100)
                                    .Parent<IInitCommandRequestBuilder>()
                                .WithInstanceNumber(1)
                                .WithNamingPattern("#{name}#{instance}")
                                .Build();
        }

        [TestMethod]
        [DynamicData(nameof(Test_Data), DynamicDataSourceType.Method)]
        public async Task Test_Complete_Workflow(
            IInitCommandRequest initRequest,
            IList<IDefineMachineTypeAddCommandRequest> defineMachineTypeAddCommandRequestsList,
            IList<IDefineMachineAddCommandRequest> defineMachineAddCommandRequestsList,
            IList<IStatusCommandRequest> statusBeforeUpCommandRequestsList,
            IList<IUpCommandRequest> upRequestsList,
            IList<IStatusCommandRequest> statusAfterUpCommandRequestsList,
            IList<ISshConfigCommandRequest> sshConfigCommandRequestsList,
            IList<ISshCommandRequest> sshCommandRequestsList,
            IList<IHaltCommandRequest> haltRequestsList,
            IList<IDestroyCommandRequest> destroyRequestsLists,
            bool startCode,
            bool cleanAtEnd,
            bool runOnlyInit
        )
        {
            if (
                null == _initCommand
                || null == _defineMachineAddCommand
                || null == _defineMachineTypeAddCommand
                || null == _statusCommand
                || null == _upCommand
                || null == _sshCommand
                || null == _sshConfigCommand
                || null == _haltCommand
                || null == _destroyCommand
                || null == _nameCommand
                || null == _nameCommandRequestBuilderFactory
            )
            {
                throw new InvalidOperationException("null");
            }


            TestInner(await _initCommand.Execute(initRequest));

            if (startCode)
            {
                Process.Start("C:\\Program Files\\Microsoft VS Code\\Code.exe", "-n " + initRequest.Base.WorkingDirectory);
            }

            foreach (var item in defineMachineTypeAddCommandRequestsList)
            {
                TestInner(await _defineMachineTypeAddCommand.Execute(item));
            }

            foreach (var item in defineMachineAddCommandRequestsList)
            {
                TestInner(await _defineMachineAddCommand.Execute(item));
            }

            if (!runOnlyInit)
            {
                foreach (var item in statusBeforeUpCommandRequestsList)
                {
                    var statusResponse = await _statusCommand.Execute(item);
                    Assert.IsNotNull(statusResponse);
                    Assert.IsTrue(statusResponse.Statuses.Any());

                    foreach (var statusItem in statusResponse.Statuses)
                    {
                        Assert.AreEqual(VagrantMachineStatusEnum.NOT_CREATED, statusItem.Value);
                    }
                }

                var willBeUp = new List<string>();

                foreach (var item in upRequestsList)
                {
                    var upResponse = await _upCommand.Execute(item);
                    TestInner(upResponse);
                    Assert.IsNotNull(upResponse.Response);
                    Assert.IsNotNull(upResponse.Response?.ProcessExecutionResult?.WaitForCompleteExit);
                    await upResponse.Response.ProcessExecutionResult.WaitForCompleteExit;

                    var realNames = await _nameCommand.Execute(_nameCommandRequestBuilderFactory.Factory()
                        .BaseBuilder
                            .UsingWorkingDirectory(item.Base.WorkingDirectory)
                            .Parent<INameCommandRequestBuilder>()
                        .WithNames(item.Names)
                        .Build());

                    willBeUp.AddRange(realNames.Names);
                }

                foreach (var item in statusAfterUpCommandRequestsList)
                {
                    var statusResponse = await _statusCommand.Execute(item);
                    Assert.IsNotNull(statusResponse);
                    Assert.IsTrue(statusResponse.Statuses.Any());

                    foreach (var (key, value) in statusResponse.Statuses)
                    {
                        Assert.AreEqual(
                            willBeUp.Contains(key)
                                ? VagrantMachineStatusEnum.RUNNING
                                : VagrantMachineStatusEnum.NOT_CREATED, value);
                    }
                }

                foreach (var item in sshConfigCommandRequestsList)
                {
                    TestInner(await _sshConfigCommand.Execute(item));
                }

                foreach (var item in sshCommandRequestsList)
                {
                    TestInner(await _sshCommand.Execute(item));
                }

                foreach (var item in haltRequestsList)
                {
                    TestInner(await _haltCommand.Execute(item));
                }

                foreach (var item in destroyRequestsLists)
                {
                    TestInner(await _destroyCommand.Execute(item));
                }
            }

            // generic asserts
            Assert.IsTrue(Directory.Exists(initRequest.Base.WorkingDirectory));
            Assert.IsTrue(File.Exists(Path.Join(initRequest.Base.WorkingDirectory, "Vagrantfile")));

            if (cleanAtEnd)
            {
                //clean
                Directory.Delete(initRequest.Base.WorkingDirectory, true);
                Assert.IsFalse(Directory.Exists(initRequest.Base.WorkingDirectory));
            }
        }

        private static void TestInner(IRootCommandResponse response)
        {
            Assert.IsNotNull(response);
        }
    }
}
