namespace CompanyPortal.Core.Common;

public record EnumToRecord<T>(string DisplayName, T Value) where T : Enum;
