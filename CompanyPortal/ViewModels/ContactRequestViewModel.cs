using System.ComponentModel.DataAnnotations;
using CompanyPortal.Core.Common;

namespace CompanyPortal.ViewModels;

public class ContactRequestViewModel : ViewModelBase
{
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

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
