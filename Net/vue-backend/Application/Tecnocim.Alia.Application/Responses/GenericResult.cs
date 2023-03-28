namespace Tecnocim.Alia.Application.Responses;

public class GenericResult<T>
{
    public int ErrorCode { get; init; }
    public string? ErrorDetails { get; init; }
    public bool IsSuccessful { get; init; }
    public T? Result { get; init; }

    public GenericResult<T> Failed(int errorCode, string? errorDetails = null) => new()
    {
        ErrorCode = errorCode,
        ErrorDetails = errorDetails,
        IsSuccessful = false,
        Result = default
    };

    public GenericResult<T> Ok(T resultEntity)
    {
        return new()
        {
            ErrorCode = 200,
            IsSuccessful = true,
            Result = resultEntity
        };
    }

    public GenericResult<T> NotFound()
    {
        return new()
        {
            ErrorCode = 404,
            IsSuccessful = true,
            Result = default
        };
    }
}
