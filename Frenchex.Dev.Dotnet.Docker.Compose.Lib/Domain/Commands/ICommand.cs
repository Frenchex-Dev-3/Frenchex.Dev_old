namespace Frenchex.Dev.Dotnet.Docker.Compose.Lib.Domain.Commands;

public interface ICommand<in TR,T>
{
    T Execute(TR request);
}

public interface ICommandAsync<in TR,T>
{
    Task<T> ExecuteAsync(TR request);
}