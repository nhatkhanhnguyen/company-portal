using CompanyPortal.Core.Extensions;
using Microsoft.AspNetCore.Http;

namespace CompanyPortal.Core.Providers;

public interface IUserPrincipalsProvider
{
    string? GetCurrentUserId();
}

public class UserPrincipalsProvider(IHttpContextAccessor httpContextAccessor) : IUserPrincipalsProvider
{
    public string? GetCurrentUserId()
    {
        return httpContextAccessor.HttpContext?.User.GetCurrentUserId();
    }
}