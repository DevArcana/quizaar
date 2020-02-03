using System;

namespace Application.Common.ErrorHandling
{
    public abstract class ResultBase
    {
        private readonly Error? _error;

        public Error Error
        {
            get
            {
                if (!_error.HasValue) throw new InvalidOperationException("Cannot access error message of a successful operation.");
                return _error.Value;
            }
        }

        public bool Succeeded => !_error.HasValue;

        protected internal ResultBase(Error? error)
        {
            _error = error;
        }
    }

    public class Result : ResultBase
    {
        public Result(Error? error) : base(error)
        {

        }

        public static Result Ok() => new Result(null);
        public static Result<T> Ok<T>(T value) => new Result<T>(value, null);
        public static Result Failure(ErrorType errorType, string message) => new Result(new Error(errorType, message));
        public static Result Failure(string message) => new Result(new Error(message));
    }

    public class Result<T> : ResultBase
    {
        private readonly T _value;

        public T Value
        {
            get
            {
                if (!Succeeded) throw new InvalidOperationException("Cannot access value of a failed operation!");
                return _value;
            }
        }

        public Result(T value, Error? error) : base(error)
        {
            _value = value;
        }

        public static implicit operator Result<T>(Result result) => new Result<T>(default, result.Succeeded ? (Error?) null : result.Error);
    }
}
