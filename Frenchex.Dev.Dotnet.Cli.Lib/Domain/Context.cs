﻿namespace Frenchex.Dev.Dotnet.Cli.Lib.Domain;

public class Context
{
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

    public string HostSettings { get; } = "hostsettings.json";
    public string AppSettings { get; } = "appsettings.json";
    public string Prefix { get; }
    public string BasePath { get; }
}