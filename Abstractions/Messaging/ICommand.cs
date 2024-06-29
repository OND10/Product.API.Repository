
using MediatR;
using ProductAPI.VSA.ExceptionHandler;

namespace ProductAPI.Abstractions.Messaging
{
    public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
