namespace Frenchex.Dev.Dotnet.Filesystem.Lib.Domain;

public interface IFilesystem
{
    DirectoryInfo CreateDirectory(string? path);
    bool DirectoryExists(string? path);
    void CopyFile(string sourceFile, string destinationFile);
    Task WriteAllTextAsync(string path, string content);
    void CopyDirectory(string source, string target);
    bool FileExists(string path);
}