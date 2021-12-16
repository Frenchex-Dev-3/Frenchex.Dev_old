using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Frenchex.Dev.Vos.Lib.Domain.Bases
{
    public class MachineBaseDefinition
    {
        public MachineBaseDefinition(
            int? virtualCpus,
            OsTypeEnum? osType,
            string? osVersion,
            int? ramInMB,
            int? videoRamInMB,
            bool? enable3D,
            string? biosLogoImagePath,
            bool? pageFusion,
            bool? gui,
            ProviderEnum? provider,
            bool? enabled,
            string? box,
            Dictionary<string, ProvisioningDefinition>? provisioning,
            Dictionary<string, FileCopyDefinition>? files,
            Dictionary<string, SharedFolderDefinition>? sharedFolders
        )
        {
            VirtualCpus = virtualCpus;
            OsType = osType;
            OsVersion = osVersion;
            RamInMB = ramInMB;
            VideoRamInMB = videoRamInMB;
            Enable3D = enable3D;
            BiosLogoImagePath = biosLogoImagePath;
            PageFusion = pageFusion;
            Gui = gui;
            Provider = provider;
            Enabled = enabled;
            Box = box;
            Provisioning = provisioning;
            Files = files;
            SharedFolders = sharedFolders;
        }

        [JsonProperty("vcpus")]
        public int? VirtualCpus { get; }

        [JsonProperty("os_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OsTypeEnum? OsType { get; }

        [JsonProperty("os_version")]
        public string? OsVersion { get; }

        [JsonProperty("ram_mb")]
        public int? RamInMB { get; }

        [JsonProperty("vram_mb")]
        public int? VideoRamInMB { get; }

        [JsonProperty("enable_3D")]
        public bool? Enable3D { get; }

        [JsonProperty("bios_logo_image_path")]
        public string? BiosLogoImagePath { get; }

        [JsonProperty("pagefusion")]
        public bool? PageFusion { get; }

        [JsonProperty("gui")]
        public bool? Gui { get; }

        [JsonProperty("provider")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProviderEnum? Provider { get; }

        [JsonProperty("enabled")]
        public bool? Enabled { get; }

        [JsonProperty("box")]
        public string? Box { get; }

        [JsonProperty("provisioning")]
        public Dictionary<string, ProvisioningDefinition>? Provisioning { get; }

        [JsonProperty("files")]
        public Dictionary<string, FileCopyDefinition>? Files { get; }

        [JsonProperty("shared_folders")]
        public Dictionary<string, SharedFolderDefinition>? SharedFolders { get; }
    }
}
