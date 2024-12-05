using System.Net;
using System.Text.Json.Serialization;

namespace AppServices;

public class ServiceResult<T>
{
    public T? Data { get; set; }
    public List<string>? ErrorMessage { get; set; }
    [JsonIgnore] bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
    [JsonIgnore] public bool IsFailure => !IsSuccess;
    [JsonIgnore] public HttpStatusCode StatusCode { get; set; }
    [JsonIgnore] public string? UrlAsCreated { get; set; }

    // static factory methods
    public static ServiceResult<T> Success(T data, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new ServiceResult<T>()
        {
            Data = data,
            StatusCode = statusCode
        };
    }
    public static ServiceResult<T> SuccessAsCreated(T data, string urlAsCreated)
    {
        return new ServiceResult<T>()
        {
            Data = data,
            StatusCode = HttpStatusCode.Created,
            UrlAsCreated = urlAsCreated
        };
    }

    public static ServiceResult<T> Failure(List<string> errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T>()
        {
            ErrorMessage = errorMessage,
            StatusCode = statusCode
        };
    }
    public static ServiceResult<T> Failure(string errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T>()
        {
            ErrorMessage = [errorMessage],                  //.net 8 ile gelen özellik
            StatusCode = statusCode
        };
    }
}

public class ServiceResult
{
    public List<string>? ErrorMessage { get; set; }
    public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
    public bool IsFailure => !IsSuccess;
    public HttpStatusCode StatusCode { get; set; }

    // static factory methods
    public static ServiceResult Success(HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new ServiceResult()
        {
            StatusCode = statusCode
        };
    }
    public static ServiceResult Failure(List<string> errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult()
        {
            ErrorMessage = errorMessage,
            StatusCode = statusCode
        };
    }
    public static ServiceResult Failure(string errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult()
        {
            ErrorMessage = [errorMessage],                  //.net 8 ile gelen özellik
            StatusCode = statusCode
        };
    }
}