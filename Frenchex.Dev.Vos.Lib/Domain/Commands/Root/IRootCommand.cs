namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Root
{
    public interface IRootCommand<U, R>
        where U : IRootCommandRequest
        where R : IRootCommandResponse
    {
        Task<R> Execute(U request);
    }
}
