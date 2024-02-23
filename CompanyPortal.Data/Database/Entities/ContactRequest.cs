using CompanyPortal.Core.Common;

namespace CompanyPortal.Data.Database.Entities;

public class ContactRequest : EntityBase
{
    public string Fullname { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public bool IsRead { get; set; } = false;

    public bool IsResponsed { get; set; } = false;
}
