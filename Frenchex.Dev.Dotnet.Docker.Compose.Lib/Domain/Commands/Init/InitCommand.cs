using System.Text.Json;
using Frenchex.Dev.Dotnet.Filesystem.Lib.Domain;
using Frenchex.Dev.Dotnet.Process.Lib.Domain.ProcessBuilder;
using Microsoft.Extensions.Configuration;

namespace Frenchex.Dev.Dotnet.Docker.Compose.Lib.Domain.Commands.Init;

public interface IInitCommand : ICommandAsync<InitCommandRequest, InitCommandResponse>
{
}

public class InitCommand : IInitCommand
{
    private readonly IConfiguration _configuration;
    private readonly IFilesystem _fileSystem;
    private readonly IProcessBuilder _processBuilder;

    public InitCommand(
        IProcessBuilder processBuilder,
        IFilesystem fileSystem,
        IConfiguration configuration
    )
    {
        _processBuilder = processBuilder;
        _fileSystem = fileSystem;
        _configuration = configuration;
    }

    public async Task<InitCommandResponse> ExecuteAsync(InitCommandRequest request)
    {
        if (null == request.WorkingDirectory) throw new ArgumentNullException(nameof(request.WorkingDirectory));

        if (0 == request.TimeoutMs) request.TimeoutMs = (int) TimeSpan.FromMinutes(1).TotalMilliseconds;

        if (!_fileSystem.DirectoryExists(request.WorkingDirectory)
            && !_fileSystem.FileExists(request.WorkingDirectory))
            _fileSystem.CreateDirectory(request.WorkingDirectory);

        var serialized = JsonSerializer.Serialize(request.Declaration, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        await _fileSystem
            .WriteAllTextAsync(
                Path.Join(request.WorkingDirectory, "docker-compose.yml"),
                serialized
            );

        return new InitCommandResponse();
    }
}