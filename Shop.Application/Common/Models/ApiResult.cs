namespace Shop.Application.Common.Models;

public sealed class ApiResult<T> : IDisposable
{
    public string Message { get; set; }
    public T? Data { get; set; }
    public string[]? Errors { get; set; }
    public ResponseTypeEnum Type { get; set; }

    public ApiResult(string message, ResponseTypeEnum responseTypeEnum)
    {
        Message = message;
        Type = responseTypeEnum;
    }

    public ApiResult(T? data, ResponseTypeEnum responseTypeEnum, string message = "", string[]? errors = null)
    {
        Data = data;
        Message = message;
        Errors = errors;
        Type = responseTypeEnum;
    }

    

    public void Dispose()
    {
        if (Data != null && typeof(T).GetInterfaces().Contains(typeof(IDisposable)))
        {
            ((IDisposable)Data).Dispose();
        }
    }
}

public sealed class ApiResult
{
    public string Message { get; set; }
    public dynamic Data { get; set; }
    public string[]? Errors { get; set; }
    public ResponseTypeEnum Type { get; set; }
    

    public ApiResult(string message, ResponseTypeEnum responseTypeEnum)
    {
        Message = message;
        Type = responseTypeEnum;
    }

    public ApiResult(dynamic data, ResponseTypeEnum responseTypeEnum, string message = "", string[]? errors = null)
    {
        Data = data;
        Message = message;
        Errors = errors;
        Type = responseTypeEnum;
    }
}

public enum ResponseTypeEnum
{
    Success = 1,    
    Warning = 2,
    Error = 3
}