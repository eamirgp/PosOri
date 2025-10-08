namespace Pos.Application.Shared.Result
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public List<string>? Errors { get; }
        public int StatusCode { get; }

        private Result(bool isSuccess, T? value, List<string>? errors, int statusCode)
        {
            IsSuccess = isSuccess;
            Value = value;
            Errors = errors;
            StatusCode = statusCode;
        }

        public static Result<T> Success(T value, int statusCode = 200) => new(true, value, null, statusCode);
        public static Result<T> Failure(List<string> errors, int statusCode = 400) => new(false, default, errors, statusCode);
    }
}
