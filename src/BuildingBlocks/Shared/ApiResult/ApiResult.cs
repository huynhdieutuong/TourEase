using System.Text.Json.Serialization;

namespace BuildingBlocks.Shared.ApiResult;
public class ApiResult<T>
{
    public T Data { get; set; }
    public string Message { get; set; }
    public bool IsSucceeded { get; set; }

    public ApiResult(bool isSucceeded, string message = null)
    {
        Message = message;
        IsSucceeded = isSucceeded;
    }

    [JsonConstructor]
    public ApiResult(bool isSucceeded, T data, string message = null)
    {
        Data = data;
        Message = message;
        IsSucceeded = isSucceeded;
    }
}
