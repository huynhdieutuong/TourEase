namespace BuildingBlocks.Shared.ApiResult;
public class ApiErrorResult<T> : ApiResult<T>
{
    public List<string> Errors { get; set; }

    public ApiErrorResult() : this("Something wrong happened. Please try again later")
    {
    }

    public ApiErrorResult(string message) : base(false, message)
    {
    }


    public ApiErrorResult(List<string> errors) : base(false)
    {
        Errors = errors;
    }
}
