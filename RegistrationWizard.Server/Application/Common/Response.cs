namespace RegistrationWizard.Server.Application.Common;

public enum HandlerResultType
{
    Success,
    Conflict,
    InvalidRequest,
    NotFound
}

public class HandlerResult
{
    public string? ErrorMessage { get; }
    public HandlerResultType ResultType { get; }

    protected HandlerResult(string? errorMessage, HandlerResultType resultType)
    {
        ErrorMessage = errorMessage;
        ResultType = resultType;
    }

    public static HandlerResult Success() =>
        new HandlerResult( null, HandlerResultType.Success);

    public static HandlerResult Conflict(string errorMessage) =>
        new HandlerResult(errorMessage, HandlerResultType.Conflict);

    public static HandlerResult Invalid(string errorMessage) =>
        new HandlerResult(errorMessage, HandlerResultType.InvalidRequest);
}

public class HandlerResult<T> : HandlerResult
{
    public T? Data { get; }

    protected HandlerResult(
        T? data,
        string? errorMessage,
        HandlerResultType resultType) : base(errorMessage, resultType)
    {
        Data = data;
    }

    public static HandlerResult<T> Success(T data) =>
        new(data, null, HandlerResultType.Success);
}
