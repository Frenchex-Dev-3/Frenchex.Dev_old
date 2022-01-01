using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Init;

public interface IInitCommandRequest : IRootCommandRequest
{
    string NamingPattern { get; }
    int LeadingZeroes { get; }
}