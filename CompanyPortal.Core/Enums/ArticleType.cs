using System.ComponentModel;

namespace CompanyPortal.Core.Enums;

public enum ArticleType
{
    [Description("Tin tức")]
    News = 1,

    [Description("Giới thiệu")]
    About = 2,

    [Description("Công nghệ sản xuất")]
    Technology = 3,

    [Description("Tư vấn kỹ thuật")]
    Technical = 4,

    [Description("Tại sao")]
    WhyUs = 5,

    [Description("Khác")]
    Other
}
