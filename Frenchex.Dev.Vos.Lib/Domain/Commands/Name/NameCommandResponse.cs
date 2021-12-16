namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Name
{
    public class NameCommandResponse : INameCommandResponse
    {
        public string[] Names { get; private set; }

        public NameCommandResponse(string[] names)
        {
            Names = names;
        }
    }
}
