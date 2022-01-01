using Frenchex.Dev.Vos.Lib.Domain.Definitions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Frenchex.Dev.Vos.Lib.Domain.Configuration;

public class MachineBaseDefinitionDeclaration
{
    [JsonProperty("vcpus")] public int? VirtualCpus { get; set; }

    [JsonProperty("os_type")]
    [JsonConverter(typeof(StringEnumConverter))]
    public OsTypeEnum? OsType { get; set; }

    [JsonProperty("os_version")] public string? OsVersion { get; set; }

    [JsonProperty("ram_mb")] public int? RamInMb { get; set; }

    [JsonProperty("vram_mb")] public int? VideoRamInMb { get; set; }

    [JsonProperty("enable_3D")] public bool? Enable3D { get; set; }

    [JsonProperty("bios_logo_image_path")] public string? BiosLogoImagePath { get; set; }

    [JsonProperty("pagefusion")] public bool? PageFusion { get; set; }

    [JsonProperty("gui")] public bool? Gui { get; set; }

    [JsonProperty("provider")]
    [JsonConverter(typeof(StringEnumConverter))]
    public ProviderEnum? Provider { get; set; }

    [JsonProperty("enabled")] public bool? Enabled { get; set; }

    [JsonProperty("box")] public string? Box { get; set; }

    [JsonProperty("provisioning")]
    public Dictionary<string, ProvisioningDefinition>? Provisioning { get; set; } = new();

    [JsonProperty("files")] public Dictionary<string, FileCopyDefinition>? Files { get; set; } = new();

    [JsonProperty("shared_folders")]
    public Dictionary<string, SharedFolderDefinition>? SharedFolders { get; set; } = new();
}