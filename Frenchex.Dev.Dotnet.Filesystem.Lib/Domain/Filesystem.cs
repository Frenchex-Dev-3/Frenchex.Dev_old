namespace Frenchex.Dev.Dotnet.Filesystem.Lib.Domain
{
    public class Filesystem : IFilesystem
    {
        public void CopyFile(string v1, string v2)
        {
            File.Copy(v1, v2);
        }

        public DirectoryInfo CreateDirectory(string path)
        {
            return Directory.CreateDirectory(path);
        }

        public async Task WriteAllTextAsync(string path, string content)
        {
            await File.WriteAllTextAsync(path, content);
        }

        public void DeleteDirectory(string path)
        {
            Directory.Delete(path, true);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }
    }
}
