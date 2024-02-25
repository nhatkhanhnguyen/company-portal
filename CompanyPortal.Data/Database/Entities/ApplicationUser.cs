using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations;

namespace CompanyPortal.Data.Database.Entities;

public class ApplicationUser : IdentityUser
{
    [MaxLength(50)]
    public string Firstname { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Surname { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Lastname { get; set; } = string.Empty;
}
