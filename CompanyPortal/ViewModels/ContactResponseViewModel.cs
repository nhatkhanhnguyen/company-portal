using System.ComponentModel;

namespace CompanyPortal.ViewModels;

public class ContactResponseViewModel
{
    [DisplayName("Nội dung")]
    public string ResponseMessage { get; set; } = string.Empty;
}
