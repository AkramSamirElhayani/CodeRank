namespace CodeRank.Domain.Abstractions;


public class Result
{

    protected Result(ResultType status, Error[] errors)
    {
        Status = status;
        Errors = errors;
    }

    public ResultType Status { get; }

    public bool IsSuccess { get => Status != ResultType.Failure; }
    public bool IsFailure { get => !IsSuccess; }
    public Error[] Errors { get; }
    public Error? Error => Errors.Length>0? Errors[0]:null;
    

    public static Result Success() => new Result(ResultType.Success, new Error[0]);
    public static Result<TValue> Success<TValue>(TValue value) => new Result<TValue>(value, ResultType.Success, new Error[0]);
    public static Result Failure(params Error[] errors) => new Result(ResultType.Failure, errors);
    public static Result<TValue> Failure<TValue>(params Error[] errors) => new Result<TValue>(default!, ResultType.Failure, errors);


    public static Result FirstFailureOrSuccess(params Result[] results)
    {
        foreach (Result result in results)
        {
            if (result.IsFailure)
            {
                return result;
            }
        }

        return Success();
    }


}
public class Result<TValue> : Result
{
    private readonly TValue _value;
    public Result(TValue value, ResultType status, Error[] errors) : base(status, errors)
    {
        _value = value;
    }


    public TValue Value
    {
        get => IsSuccess
            ? _value
            : default!;
    }

    public static implicit operator Result<TValue>(TValue value) => Success(value);

}
