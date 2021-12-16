﻿using Frenchex.Dev.Vos.Lib.Domain.Actions.Configuration.Load;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Naming;
using Frenchex.Dev.Vos.Lib.Domain.Bases;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Root
{
    abstract public class RootCommand
    {
        protected readonly IConfigurationLoadAction _configurationLoadAction;
        protected readonly IVexNameToVagrantNameConverter _nameToVagrantNameConverter;
        public RootCommand(
            IConfigurationLoadAction configurationLoadAction,
            IVexNameToVagrantNameConverter nameConverter
        )
        {
            _configurationLoadAction = configurationLoadAction;
            _nameToVagrantNameConverter = nameConverter;
        }

        protected async Task<Configuration> ConfigurationLoad(string path)
        {
            return await _configurationLoadAction.Load(Path.Join(path, "config.json"));
        }

        protected string[] MapNamesToVagrantNames(
            string[] names,
            string workingDirectory,
            Configuration configuration
        )
        {
            return _nameToVagrantNameConverter.ConvertAll(names, workingDirectory, configuration);
        }
    }
}