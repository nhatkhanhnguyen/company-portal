namespace CompanyPortal.Core.Common;

public record EnumToRecord<T>(int? Index = null, string? DisplayName = "", T? Value = default, double? CustomValue = null, string? Icon = null) where T : Enum;
