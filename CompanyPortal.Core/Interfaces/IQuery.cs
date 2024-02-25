using MediatR;

namespace CompanyPortal.Core.Interfaces;

public interface IQuery<TResponse> : IRequest<TResponse>;

public interface ICachedQuery<TResponse> : IQuery<TResponse>, ICachedQuery;

public interface ICachedQuery
{
    bool ForceLiveData { get; }
    string Key { get; }
    TimeSpan? Expiration { get; }
}
