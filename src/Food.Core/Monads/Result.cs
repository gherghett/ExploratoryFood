namespace Food.Core;

public class Result<T>
{
    public bool IsSuccess {get;}
    public T? Value {get;}

    public string? FailMessage {get;}
    public Result(T value)
    {
        IsSuccess = value is null ? false : true;
        Value = value;
    }

    public Result()
    {
        IsSuccess = false;
    }

    public Result(string failMessage)
    {
        IsSuccess = false;
        FailMessage = failMessage;
    }

    public bool TryGet(out T var)
    {
        if( IsSuccess && Value is not null)
        {
            var = Value;
            return true;
        }
        
        var = default(T)!;
        return false;
    }
}
