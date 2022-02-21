namespace Frenchex.Dev.Dotnet.Filesystem.Lib.Domain;

public class Filesystem : IFilesystem
{
    public void CopyFile(string v1, string v2)
    {
        File.Copy(v1, v2);
    }

    public DirectoryInfo CreateDirectory(string? path)
    {
        if (string.IsNullOrEmpty(path)) 
            throw new ArgumentNullException(nameof(path));

        return Directory.CreateDirectory(path);
    }

    public async Task WriteAllTextAsync(string path, string content)
    {
        await File.WriteAllTextAsync(path, content);
    }

    public void CopyDirectory(string source, string target)
    {
        if (!Directory.Exists(target))
            Directory.CreateDirectory(target);

        var files = Directory.GetFiles(source);
        foreach (var file in files)
        {
            var name = Path.GetFileName(file);
            var dest = Path.Combine(target, name);
            File.Copy(file, dest);
        }

        var folders = Directory.GetDirectories(source);
        foreach (var folder in folders)
        {
            var name = Path.GetFileName(folder);
            var dest = Path.Combine(target, name);
            CopyDirectory(folder, dest);
        }
    }

    public bool DirectoryExists(string? path)
    {
        return Directory.Exists(path);
    }
}