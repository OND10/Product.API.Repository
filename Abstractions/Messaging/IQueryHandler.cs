﻿using OnMapper.Common.Exceptions;

namespace ProductAPI.Abstractions.Messaging
{
    public interface IQueryHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken);
    }
}
