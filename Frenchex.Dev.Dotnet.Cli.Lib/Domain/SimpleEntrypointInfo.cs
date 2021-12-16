﻿namespace Frenchex.Dev.Dotnet.Cli.Lib.Domain
{
    /// <summary>Implements <see cref="IEntrypointInfo"/> by exposing provided data.</summary>
    public class SimpleEntrypointInfo : IEntrypointInfo
    {
        public SimpleEntrypointInfo(string commandLine, string[] commandLineArgs)
        {
            CommandLine = commandLine ?? throw new ArgumentNullException(nameof(commandLine));
            CommandLineArgs = commandLineArgs ?? throw new ArgumentNullException(nameof(commandLineArgs));
        }

        public string CommandLine { get; }

        public string[] CommandLineArgs { get; }
    }
}