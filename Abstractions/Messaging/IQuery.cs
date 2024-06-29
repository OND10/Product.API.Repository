using MediatR;
using OnMapper.Common.Exceptions;

namespace ProductAPI.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
