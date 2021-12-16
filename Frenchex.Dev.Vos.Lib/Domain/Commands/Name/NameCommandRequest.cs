using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Name
{
    public class NameCommandRequest : RootRequest, INameCommandRequest
    {
        public NameCommandRequest(
            IBaseRequest _base,
            string[] _names
        ) : base(_base)
        {
            Names = _names;
        }

        public string[] Names { get; }
    }
}
