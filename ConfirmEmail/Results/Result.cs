using ConfirmEmail.Errors;

namespace ConfirmEmail.Results;

public class Result(bool isSuccess, Error error)
{
    public bool IsSuccess { get; set; } = isSuccess;

    public Error Error { get; set; } = error;

    public static Result Success() => new(true, Error.None);

    public static Result Failure(Error error) => new(false, error);

    public TResult Match<TResult>(TResult onSuccess, TResult onFailure)
    {
        return IsSuccess ? onSuccess : onFailure;
    }
}

public class Result<T> : Result
{
    public T Value { get; }

    private Result(T value) : base(true, Error.None)
    {
        Value = value;
        IsSuccess = true;
    }

    private Result(Error error) : base(false, error)
    {
        Error = error;
        IsSuccess = false;
    }

    public static Result<T> Success(T value) => new(value);

    public new static Result<T> Failure(Error error) => new(error);

    public static implicit operator Result<T>(T value) => new(value);

    public static implicit operator Result<T>(Error error) => new(error);
}