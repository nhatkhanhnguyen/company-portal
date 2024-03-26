namespace CompanyPortal.Core.Common;

public class Result
{
    public object? Data { get; }
    public Type? DataType { get; }
    public bool IsError { get; }
    public bool IsSuccess => !IsError;

    private Result(object? data, Type? resultType, bool isError)
    {
        Data = data;
        DataType = resultType;
        IsError = isError;
    }

    public static Result Ok<T>(T result) => Ok(result, typeof(T));

    public static Result Ok<T>(T result, Type? dataType) => new(result, dataType, false);

    public static Result Ok() => new(null, null, false);

    public static Result Error() => new(null, null, true);

    public static Result Error<T>(T payload) => new(payload, null, true);
}
