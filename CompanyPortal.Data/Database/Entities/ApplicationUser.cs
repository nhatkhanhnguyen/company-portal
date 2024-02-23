using Microsoft.AspNetCore.Identity;

namespace CompanyPortal.Data.Database.Entities;

public class ApplicationUser : IdentityUser
{
    public string Firstname { get; set; } = string.Empty;

    public string Lastname { get; set; } = string.Empty;
}
