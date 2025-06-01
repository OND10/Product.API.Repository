using ProductAPI.VSA.ExceptionHandler;

namespace ProductAPI.Abstractions.Messaging
{
    public interface ICommandHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken);
    }
}
