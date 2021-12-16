using Frenchex.Dev.Dotnet.Filesystem.Lib.Domain;
using System.Reflection;

namespace Frenchex.Dev.Vos.Lib.Domain.Resources
{
    public interface IVagrantfileResource
    {
        void Copy(string destination);
    }

    public class VagrantfileResource : IVagrantfileResource
    {
        private readonly IFilesystem _filesystem;
        private const string VAGRANTFILE = "Vagrantfile";
        private readonly string _sourceVagrantfile;

        public VagrantfileResource(IFilesystem filesystem)
        {
            var assembly = Assembly.GetAssembly(typeof(VagrantfileResource));
            if (null == assembly)
            {
                throw new InvalidOperationException("assembly is null");
            }

            _filesystem = filesystem;

            _sourceVagrantfile = Path.Join(
                Path.GetDirectoryName(assembly.Location),
                "Resources",
                "Vagrant",
                VAGRANTFILE
            );
        }

        public void Copy(string destination)
        {
            _filesystem
                .CopyFile(
                    _sourceVagrantfile,
                    Path.Join(destination, VAGRANTFILE)
                );
        }
    }
}
