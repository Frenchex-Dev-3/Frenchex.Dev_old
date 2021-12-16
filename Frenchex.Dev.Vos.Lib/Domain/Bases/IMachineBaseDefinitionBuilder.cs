namespace Frenchex.Dev.Vos.Lib.Domain.Bases
{
    public interface IMachineBaseDefinitionBuilderFactory
    {
        IMachineBaseDefinitionBuilder Factory();
    }

    public class MachineBaseDefinitionBuilderFactory : IMachineBaseDefinitionBuilderFactory
    {
        public IMachineBaseDefinitionBuilder Factory()
        {
            return new MachineBaseDefinitionBuilder();
        }
    }

    public interface IMachineBaseDefinitionBuilder
    {
        IMachineBaseDefinitionBuilder WithVirtualCpus(int with);
        IMachineBaseDefinitionBuilder WithOsType(OsTypeEnum type);
        IMachineBaseDefinitionBuilder WithOsVersion(string version);
        IMachineBaseDefinitionBuilder WithRamInMB(int ramInMB);
        IMachineBaseDefinitionBuilder WithVideoRamInMB(int videoRamInMB);
        IMachineBaseDefinitionBuilder With3DEnabled(bool enabled);
        IMachineBaseDefinitionBuilder WithBiosLogoImage(string path);
        IMachineBaseDefinitionBuilder WithPageFusion(bool enabled);
        IMachineBaseDefinitionBuilder WithGui(bool enabled);
        IMachineBaseDefinitionBuilder WithProvider(ProviderEnum provider);
        IMachineBaseDefinitionBuilder Enabled(bool enabled);
        IMachineBaseDefinitionBuilder WithBox(string box);
        IMachineBaseDefinitionBuilder WithProvisioning(Dictionary<string, ProvisioningDefinition> provisioning);
        IMachineBaseDefinitionBuilder WithFiles(Dictionary<string, FileCopyDefinition> files);
        IMachineBaseDefinitionBuilder WithSharedFolders(Dictionary<string, SharedFolderDefinition> sharedFolders);
        MachineBaseDefinition Build();
        T Parent<T>();
        IMachineBaseDefinitionBuilder SetParent<T>(T parent);
    }

    public class MachineBaseDefinitionBuilder : IMachineBaseDefinitionBuilder
    {

        public MachineBaseDefinitionBuilder()
        {

        }


        private object? _parent;
        public IMachineBaseDefinitionBuilder SetParent<T>(T parent)
        {
            _parent = parent;
            return this;
        }

        public T Parent<T>()
        {
            if (null == _parent)
            {
                throw new InvalidOperationException("parent is null");
            }

            return (T)_parent;
        }

        public MachineBaseDefinition Build()
        {
            if (_ramInMB == 0)
            {
                throw new Exception("RAM cannot be 0");
            }
            return new MachineBaseDefinition(
                    _vcpus ?? 1,
                    _osType ?? OsTypeEnum.Debian_64,
                    _osVersion ?? "10.9.0",
                    _ramInMB ?? 512,
                    _videoRamInMB ?? 16,
                    _enable3D ?? false,
                    _biosLogoImage ?? "",
                    _pageFusion ?? false,
                    _gui ?? false,
                    _provider ?? ProviderEnum.virtualbox,
                    _enabled ?? true,
                    _box ?? "debian/buster64",
                    _provisioning ?? new Dictionary<string, ProvisioningDefinition>(),
                    _files ?? new Dictionary<string, FileCopyDefinition>(),
                    _sharedFolders ?? new Dictionary<string, SharedFolderDefinition>()
                );
        }

        private bool? _enabled;
        public IMachineBaseDefinitionBuilder Enabled(bool enabled)
        {
            _enabled = enabled;
            return this;
        }

        private bool? _enable3D;
        public IMachineBaseDefinitionBuilder With3DEnabled(bool enabled)
        {
            _enable3D = enabled;
            return this;
        }

        private string? _biosLogoImage;
        public IMachineBaseDefinitionBuilder WithBiosLogoImage(string path)
        {
            _biosLogoImage = path;
            return this;
        }
        private string? _box;

        public IMachineBaseDefinitionBuilder WithBox(string box)
        {
            _box = box;
            return this;
        }

        private Dictionary<string, FileCopyDefinition>? _files;
        public IMachineBaseDefinitionBuilder WithFiles(Dictionary<string, FileCopyDefinition> files)
        {
            if (files is null)
            {
                throw new ArgumentNullException(nameof(files));
            }

            _files = files;
            return this;
        }

        private bool? _gui;

        public IMachineBaseDefinitionBuilder WithGui(bool enabled)
        {
            _gui = enabled;
            return this;
        }

        private OsTypeEnum? _osType;
        public IMachineBaseDefinitionBuilder WithOsType(OsTypeEnum type)
        {
            _osType = type;
            return this;
        }

        private string? _osVersion;
        public IMachineBaseDefinitionBuilder WithOsVersion(string version)
        {
            _osVersion = version;
            return this;
        }

        private bool? _pageFusion;
        public IMachineBaseDefinitionBuilder WithPageFusion(bool enabled)
        {
            _pageFusion = enabled;
            return this;
        }

        private ProviderEnum? _provider;
        public IMachineBaseDefinitionBuilder WithProvider(ProviderEnum provider)
        {
            _provider = provider;
            return this;
        }

        private Dictionary<string, ProvisioningDefinition>? _provisioning;
        public IMachineBaseDefinitionBuilder WithProvisioning(Dictionary<string, ProvisioningDefinition> provisioning)
        {
            if (provisioning is null)
            {
                throw new ArgumentNullException(nameof(provisioning));
            }

            _provisioning = provisioning;
            return this;
        }

        private int? _ramInMB;
        public IMachineBaseDefinitionBuilder WithRamInMB(int ramInMB)
        {
            _ramInMB = ramInMB;
            return this;
        }

        private Dictionary<string, SharedFolderDefinition>? _sharedFolders;
        public IMachineBaseDefinitionBuilder WithSharedFolders(Dictionary<string, SharedFolderDefinition> sharedFolders)
        {
            if (sharedFolders is null)
            {
                throw new ArgumentNullException(nameof(sharedFolders));
            }

            _sharedFolders = sharedFolders;
            return this;
        }

        private int? _videoRamInMB;
        public IMachineBaseDefinitionBuilder WithVideoRamInMB(int videoRamInMB)
        {
            _videoRamInMB = videoRamInMB;
            return this;
        }

        private int? _vcpus;
        public IMachineBaseDefinitionBuilder WithVirtualCpus(int vcpus)
        {
            _vcpus = vcpus;
            return this;
        }
    }
}
