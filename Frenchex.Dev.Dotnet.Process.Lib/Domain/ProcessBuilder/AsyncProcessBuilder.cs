using Frenchex.Dev.Dotnet.Process.Lib.Domain.Process;
using Microsoft.Extensions.Logging;

/// <summary>
/// Taken from https://gist.github.com/AlexMAS/276eed492bc989e13dcce7c78b9e179d
/// Thank you AlexMAS !
/// </summary>
namespace Frenchex.Dev.Dotnet.Process.Lib.Domain.ProcessBuilder
{
    public class AsyncProcessBuilder : IProcessBuilder
    {
        private readonly ILogger<IProcess> _processLogger;

        public AsyncProcessBuilder(
            ILogger<IProcess> processLogger
            )
        {
            _processLogger = processLogger;
        }

        public IProcess Build(IProcessBuildingParameters parameters)
        {
            var wrappedProcess = new System.Diagnostics.Process()
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = parameters.Command,
                    Arguments = parameters.Arguments,
                    WorkingDirectory = parameters.WorkingDirectory,
                    UseShellExecute = parameters.UseShellExecute,
                    RedirectStandardInput = parameters.RedirectStandardInput,
                    RedirectStandardOutput = parameters.RedirectStandardOutput,
                    RedirectStandardError = parameters.RedirectStandardError,
                    CreateNoWindow = parameters.CreateNoWindow
                }
            };

            wrappedProcess.EnableRaisingEvents = true;

            var wrapper = new Process.Process(
                wrappedProcess,
                parameters,
                _processLogger
            );

            // If you run bash-script on Linux it is possible that ExitCode can be 255.
            // To fix it you can try to add '#!/bin/bash' header to the script.

            return wrapper;
        }
    }
}