using CompanyPortal.Core.Interfaces;

using MediatR;

namespace CompanyPortal.Core.Behaviors;

public sealed class QueryCachingPipelineBehavior<TRequest, TResponse>(ICacheService cacheService)
    : IPipelineBehavior<TRequest, TResponse?> where TRequest : ICachedQuery
{
    public async Task<TResponse?> Handle(TRequest request, RequestHandlerDelegate<TResponse?> next, CancellationToken cancellationToken)
    {
        if (request.ForceLiveData)
        {
            return await next();
        }
        return await cacheService.GetOrCreateAsync(request.Key, _ => next(), request.Expiration, cancellationToken);
    }
}
