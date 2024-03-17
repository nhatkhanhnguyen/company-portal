namespace CompanyPortal.Core.Common;


public class Result<T>(T data, bool isError) : Result(data, typeof(T), isError)
    where T : new()
{
    public new T Data { get; } = data;
}

public class Result(object? data, Type? resultType, bool isError)
{
    public object? Data { get; } = data;
    public Type? DataType { get; } = resultType;
    public bool IsError { get; } = isError;
    public bool IsSuccess => !IsError;

    public static Result Ok<T>(T result) => Ok(result, typeof(T));

    public static Result Ok<T>(T result, Type? dataType) => new(result, dataType, false);

    public static Result Ok() => new(null, null, false);

    public static Result Error() => new(null, null, true);

    public static Result Error<T>(T payload) => new(payload, null, true);
}
