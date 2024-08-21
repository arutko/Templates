namespace Template.Common;

public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        if ((isSuccess && error != Error.None) || (!isSuccess && error == Error.None))
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    public static Result Failure(string code, string? description = null)
        => new(false, new Error(code, description));
}

public class Result<T>
{
    private Result(bool isSuccess, Error error, T value)
    {
        if ((isSuccess && error != Error.None) || (!isSuccess && error == Error.None))
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
        Value = value;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }
    public T Value { get; } = default!;

    public static Result<T> Success(T value)
        => new(true, Error.None, value);
    public static Result<T> Failure(Error error)
        => new(false, error, default!);
    public static Result<T> Failure(string code, string? description = null)
        => new(false, new Error(code, description), default!);
}

