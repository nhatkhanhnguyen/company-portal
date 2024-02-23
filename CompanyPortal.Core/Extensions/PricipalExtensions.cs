using System.Security.Principal;

namespace CompanyPortal.Core.Extensions;

public static class PricipalExtensions
{
    public static string? GetCurrentUserId(this IPrincipal? principal)
    {
        var userId = "";
        if (principal?.Identity != null)
        {
            userId = principal.Identity.Name;
        }

        return userId;
    }
}
