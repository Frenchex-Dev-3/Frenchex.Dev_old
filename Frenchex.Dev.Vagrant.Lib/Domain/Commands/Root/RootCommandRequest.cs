namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root
{
    public class RootCommandRequest : IRootCommandRequest
    {
        public IBaseCommandRequest Base { get; private set; }

        public RootCommandRequest(IBaseCommandRequest _base)
        {
            Base = _base;
        }
    }
}
