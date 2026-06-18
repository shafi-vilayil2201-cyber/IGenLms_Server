

namespace IGenServer.Application.Common.Responses;

public sealed class CommonResponse<T>
{
    public bool Success{get;set;}
    public string Message {get;set;}=string.Empty;
    public T? Data {get;set;}
    public List<string> Errors {get;set;}=[];

    public static CommonResponse<T> SuccessResponse(T data,string message = "")
    {
        return new CommonResponse<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    } 
    public static CommonResponse<T> FailureResponse(string message, params string[] errors)
    {
        return new CommonResponse<T>
        {
            Success = false,
            Message = message,
            Errors = errors.ToList()
        };
    }
}