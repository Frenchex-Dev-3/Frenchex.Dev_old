﻿using Frenchex.Dev.Dotnet.Filesystem.Lib.Domain;
using System.Reflection;

namespace Frenchex.Dev.Vos.Lib.Domain.Resources
{
    public interface IConfigJsonResource
    {
        void Copy(string destination);
    }

    public class ConfigJsonResource : IConfigJsonResource
    {
        private readonly IFilesystem _fileSystemOperator;
        private const string CONFIGJSON = "config.json";
        private readonly string sourceConfigJsonFile;

        public ConfigJsonResource(IFilesystem filesystem)
        {
            var assembly = Assembly.GetAssembly(typeof(VagrantfileResource));
            if (null == assembly)
            {
                throw new InvalidOperationException("assembly is null");
            }

            _fileSystemOperator = filesystem;

            sourceConfigJsonFile = Path.Join(
                Path.GetDirectoryName(assembly.Location),
                "Resources",
                "Vagrant",
                CONFIGJSON
            );
        }

        public void Copy(string destination)
        {
            _fileSystemOperator
                .CopyFile(
                    sourceConfigJsonFile,
                    Path.Join(destination, CONFIGJSON)
                );
        }
    }
}
