
namespace ProductAPI.Abstractions.Messaging;
public interface IQuery : IBaseQuery { };
public interface IQuery<TResponse> : IBaseQuery { };
public interface IBaseQuery { };
