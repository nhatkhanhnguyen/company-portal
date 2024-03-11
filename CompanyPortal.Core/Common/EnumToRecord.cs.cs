namespace CompanyPortal.Core.Common;

public record EnumToRecord<T>(string? DisplayName = "", T? Value = default, double? CustomValue = null, string? Icon = null) where T : Enum;
