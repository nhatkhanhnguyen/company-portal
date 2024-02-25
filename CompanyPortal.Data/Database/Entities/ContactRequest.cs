using CompanyPortal.Core.Common;

using System.ComponentModel.DataAnnotations;

namespace CompanyPortal.Data.Database.Entities;

public class ContactRequest : EntityBase
{
    [MaxLength(100)]
    public string Fullname { get; set; } = string.Empty;

    [MaxLength(200)]
    public string Address { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(15)]
    public string PhoneNumber { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Content { get; set; } = string.Empty;

    public bool IsRead { get; set; } = false;

    public bool IsResponsed { get; set; } = false;
}
