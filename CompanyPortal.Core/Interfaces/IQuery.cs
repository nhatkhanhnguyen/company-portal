using MediatR;

namespace CompanyPortal.Core.Interfaces;

public interface IQuery<out TResponse> : IRequest<TResponse>;

public interface ICachedQuery<out TResponse> : IQuery<TResponse>, ICachedQuery;

public interface ICachedQuery
{
    bool ForceRefresh { get; }
    string Key { get; }
    TimeSpan? Expiration { get; }
}
