namespace BuildingBlocks.Shared.ApiResult;
public class ApiSuccessResult<T> : ApiResult<T>
{
    public ApiSuccessResult(T data) : base(true, data, "Success")
    {
    }

    public ApiSuccessResult(T data, string message) : base(true, data, message)
    {
    }
}

public class ApiSuccessResult<T, M> : ApiSuccessResult<T>
{
    public M MetaData { get; set; }

    public ApiSuccessResult(T data, M metaData) : base(data)
    {
        MetaData = metaData;
    }
}
