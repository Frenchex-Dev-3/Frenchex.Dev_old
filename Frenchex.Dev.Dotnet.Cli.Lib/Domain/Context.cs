namespace Frenchex.Dev.Dotnet.Cli.Lib.Domain
{
    public class Context
    {
        public string HostSettings { get; private set; } = "hostsettings.json";
        public string AppSettings { get; private set; } = "appsettings.json";
        public string Prefix { get; private set; }
        public string BasePath { get; private set; }

        public Context(
            string hostSettings,
            string appSettings,
            string prefix,
            string basePath
        )
        {
            HostSettings = hostSettings;
            AppSettings = appSettings;
            Prefix = prefix;
            BasePath = basePath;
        }
    }
}
