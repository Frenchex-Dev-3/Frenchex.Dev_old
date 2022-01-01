using System;
using System.Collections.Generic;
using System.Linq;
using Frenchex.Dev.Vos.Lib.DependencyInjection;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Naming;
using Frenchex.Dev.Vos.Lib.Domain.Configuration;
using Frenchex.Dev.Vos.Lib.Domain.Definitions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Frenchex.Dev.Vos.Lib.Tests.Domain.Actions.Naming;

[TestClass]
public class VexNameToVagrantNameConverterTests
{
    private static IServiceProvider? _di;

    private static IVexNameToVagrantNameConverter? _vexNameToVagrantNameConverter;

    public static IEnumerable<object[]> Test_Data()
    {
        var defaultConfig = new Configuration
        {
            Vagrant = new VagrantConfiguration
            {
                PrefixWithDirBase = true,
                NumberingFormat = "{0,2:D2}"
            },
            Machines = new Dictionary<string, MachineDefinitionDeclaration>
            {
                {
                    "a-very-complex-foo", new MachineDefinitionDeclaration
                    {
                        Base = new MachineBaseDefinitionDeclaration
                        {
                            VirtualCpus = 4,
                            OsType = OsTypeEnum.Debian_64,
                            OsVersion = "10.9.0",
                            RamInMb = 128,
                            VideoRamInMb = 16,
                            Enable3D = false,
                            Enabled = true,
                            BiosLogoImagePath = null,
                            Box = "generic/alpine38",
                            Gui = false,
                            PageFusion = false,
                            Provider = ProviderEnum.virtualbox,
                            Files = new Dictionary<string, FileCopyDefinition>(),
                            SharedFolders = new Dictionary<string, SharedFolderDefinition>(),
                            Provisioning = new Dictionary<string, ProvisioningDefinition>()
                        },
                        MachineTypeName = "foo",
                        Name = "a-very-complex-foo",
                        NamingPattern = "v#{VAGRANT_INSTANCE}-#{NAME}-#{INSTANCE}",
                        Instances = 4,
                        RamInMB = 128,
                        IsPrimary = true,
                        Ipv4Pattern = "10.100.1.#{NUMBER}",
                        Ipv4Start = 3,
                        IsEnabled = true
                    }
                }
            },
            MachineTypes = new Dictionary<string, MachineTypeDefinition>
            {
                {
                    "foo", new MachineTypeDefinition
                    {
                        Name = "foo",
                        Base = new MachineBaseDefinitionDeclaration
                        {
                            VirtualCpus = 4,
                            OsType = OsTypeEnum.Debian_64,
                            OsVersion = "10.9.0",
                            RamInMb = 128,
                            VideoRamInMb = 16,
                            Enable3D = false,
                            Enabled = true,
                            BiosLogoImagePath = null,
                            Box = "generic/alpine38",
                            Gui = false,
                            PageFusion = false,
                            Provider = ProviderEnum.virtualbox,
                            Files = new Dictionary<string, FileCopyDefinition>(),
                            SharedFolders = new Dictionary<string, SharedFolderDefinition>(),
                            Provisioning = new Dictionary<string, ProvisioningDefinition>()
                        }
                    }
                }
            }
        };

        yield return new object[]
        {
            new[] {"a-very-complex-foo-[2-*]", "a-very-complex-foo-1"},
            new[] {"a-very-complex-foo-01", "a-very-complex-foo-02", "a-very-complex-foo-03"},
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
        Configuration configuration
    )
    {
        if (null == _vexNameToVagrantNameConverter)
            throw new InvalidOperationException("missing di setup");

        var converted = _vexNameToVagrantNameConverter.ConvertAll(names, workingDirectory, configuration);

        Assert.IsNotNull(converted);
        Assert.IsTrue(converted.Any());

        Assert.AreEqual(converted.Length, expectedNames.Length);

        foreach (var expected in expectedNames) Assert.IsTrue(converted.Contains(expected));
    }

    #region Static Setup

    protected static IServiceCollection SetupServices()
    {
        var services = new ServiceCollection();

        ServicesConfiguration
            .ConfigureServices(services)
            ;

        return services;
    }

    [ClassInitialize]
    public static void SetupClass(TestContext testContext)
    {
        if (testContext is null) throw new ArgumentNullException(nameof(testContext));

        _di = SetupServices().BuildServiceProvider();

        _vexNameToVagrantNameConverter = _di.GetRequiredService<IVexNameToVagrantNameConverter>();
    }

    #endregion
}