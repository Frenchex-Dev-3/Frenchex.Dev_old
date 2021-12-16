using Frenchex.Dev.Vos.Lib.Domain.Actions.Naming;
using Frenchex.Dev.Vos.Lib.Domain.Bases;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Frenchex.Dev.Vos.Lib.Tests.Domain.Actions.Naming
{


    [TestClass]
    public class VexNameToVagrantNameConverterTests
    {
        private static IServiceProvider? _di;

        private static IVexNameToVagrantNameConverter? _vexNameToVagrantNameConverter;

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

            _vexNameToVagrantNameConverter = _di.GetRequiredService<IVexNameToVagrantNameConverter>();

        }
        #endregion

        public static IEnumerable<object[]> Test_Data()
        {
            var defaultConfig = new Lib.Domain.Bases.Configuration()
            {
                Vagrant = new VagrantConfiguration()
                {
                    PrefixWithDirBase = true,
                    NumberingFormat = "{0,2:D2}"
                },
                Machines = new Dictionary<string, MachineDefinition>(){
                        { "a-very-complex-foo", new MachineDefinition(
                             @base: new MachineBaseDefinition(
                                    virtualCpus: 4,
                                    osType: OsTypeEnum.Debian_64,
                                    osVersion: "10.9.0",
                                    ramInMB: 1024,
                                    videoRamInMB: 128,
                                    enable3D: false,
                                    biosLogoImagePath: "",
                                    pageFusion: false,
                                    gui: false,
                                    provider: ProviderEnum.virtualbox,
                                    enabled: true,
                                    box: "debian/buster64",
                                    provisioning: new Dictionary<string, ProvisioningDefinition>(),
                                    files: new Dictionary<string, FileCopyDefinition>(),
                                    sharedFolders: new Dictionary<string, SharedFolderDefinition>()
                               ),
                               machineTypeName: "foo",
                               name: "a-very-complex-foo",
                               namingPattern: "v#{VAGRANT_INSTANCE}-#{NAME}-#{INSTANCE}",
                               instances: 4,
                               ramInMB: 1024,
                               virtualCPUs: 2,
                               ipv4Pattern: "10.100.1.#{NUMBER}",
                               ipv4Start: 3,
                               isPrimary: true,
                               isEnabled: true
                            ) }
                    },
                MachineTypes = new Dictionary<string, MachineTypeDefinition>(){
                        { "foo", new MachineTypeDefinition(
                               @base:  new MachineBaseDefinition(
                                    virtualCpus: 4,
                                    osType: OsTypeEnum.Debian_64,
                                    osVersion: "10.9.0",
                                    ramInMB: 1024,
                                    videoRamInMB: 128,
                                    enable3D: false,
                                    biosLogoImagePath: "",
                                    pageFusion: false,
                                    gui: false,
                                    provider: ProviderEnum.virtualbox,
                                    enabled: true,
                                    box: "debian/buster64",
                                    provisioning: new Dictionary<string, ProvisioningDefinition>(),
                                    files: new Dictionary<string, FileCopyDefinition>(),
                                    sharedFolders: new Dictionary<string, SharedFolderDefinition>()
                                ),
                                "foo"
                            ) }
                    }
            };

            yield return new object[]
            {
                new string[] { "a-very-complex-foo-[2-*]", "a-very-complex-foo-1" },
                new string[] {  "a-very-complex-foo-01", "a-very-complex-foo-02", "a-very-complex-foo-03" },
                "",
                defaultConfig
            };
        }

        [TestMethod]
        [DynamicData(nameof(Test_Data), DynamicDataSourceType.Method)]
        public void Tests(
            string[] names,
            string[] expectedNames,
            string workingDirectory,
            Lib.Domain.Bases.Configuration configuration
        )
        {
            if (null == _vexNameToVagrantNameConverter)
                throw new InvalidOperationException("missing di setup");

            var converted = _vexNameToVagrantNameConverter.ConvertAll(names, workingDirectory, configuration);

            Assert.IsNotNull(converted);
            Assert.IsTrue(converted.Any());

            Assert.AreEqual(converted.Length, expectedNames.Length);

            foreach (var expected in expectedNames)
            {
                Assert.IsTrue(converted.Contains(expected));
            }
        }
    }
}
