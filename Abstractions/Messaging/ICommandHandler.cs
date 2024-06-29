using MediatR;
using ProductAPI.VSA.ExceptionHandler;

namespace ProductAPI.Abstractions.Messaging
{
    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
        where TCommand : ICommand<TResponse>
    {
    }
}
