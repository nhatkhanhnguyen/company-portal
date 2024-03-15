using CompanyPortal.Core.Extensions;

namespace CompanyPortal.Core.Constants;

public sealed class Messages
{
    public static string ErrorWhile(string whileDoing) => $"Có lỗi xảy ra khi {whileDoing}. Vui lòng thử lại sau!";

    public static string NotFound(string something, int identifier) => $"{something.FirstCharToUpper()} có ID = {identifier} không tồn tại. Vui lòng kiểm tra!";
}

public sealed class Action
{
    private List<string> _data = [];

    public Action(List<string> data)
    {
        _data = data;
    }

    public Subject For(string subject)
    {
        return new Subject([.._data, subject]);
    }
}

public sealed class Subject
{
    private List<string> _data;

    public Subject(List<string> data)
    {
        _data = data;
    }

    public Destination To(string destination)
    {
        return new Destination([.._data, "vào", destination]);
    }

    public Destination From(string destination)
    {
        return new Destination([.. _data, "từ", destination]);
    }
}

public sealed class Destination
{
    private string _message = "Có {0} xảy ra khi đang {1} {2} {3} {4}. {5}}";
    private List<string> _data;

    public Destination(List<string> data)
    {
        _data = data;
    }

    public Destination WithAdditionalMessage(string additionalMessage)
    {
        _data.Add(additionalMessage);
        return this;
    }

    public override string ToString()
    {
        return string.Format(_message, _data.ToArray());
    }
}

public class LogMessage
{
    public static Action WarningWhile(string action)
    {
        return new Action([action]);
    }

    public static Action ErrorWhile(string action)
    {
        return new Action(["lỗi", action]);
    }
}