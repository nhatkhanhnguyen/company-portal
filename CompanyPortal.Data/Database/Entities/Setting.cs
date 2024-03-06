using System.ComponentModel.DataAnnotations;

using CompanyPortal.Core.Common;
using CompanyPortal.Core.Enums;

namespace CompanyPortal.Data.Database.Entities;

public class Setting : EntityBase
{
    [MaxLength(100)] 
    public string Name { get; set; } = string.Empty;

    public SettingType Type { get; set; }

    [MaxLength(500)]
    public string Value { get; set; } = string.Empty;

    public uint Index { get; set; }
}
