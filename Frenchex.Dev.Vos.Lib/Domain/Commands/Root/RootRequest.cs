namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Root
{
    public class RootRequest : IRootCommandRequest
    {
        public IBaseRequest Base { get; private set; }

        public RootRequest(IBaseRequest _base)
        {
            Base = _base;
        }
    }
}
