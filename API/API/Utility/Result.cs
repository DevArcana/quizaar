using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace API.Utility
{
    public class Result
    {
        public bool Success { get; }
        public bool Failure => !Success;

        public string Error { get; }

        protected Result(bool success, string error)
        {
            Success = success;
            Error = error;
        }

        public static Result Fail(string message) => new Result(false, message);
        public static Result Ok() => new Result(true, string.Empty);

        public static Result<T> Fail<T>(string message) => new Result<T>(default, false, message);
        public static Result<T> Ok<T>(T value) => new Result<T>(value, true, string.Empty);
    }

    public class Result<T> : Result
    {
        public T Value { get; }

        protected internal Result([AllowNull] T value, bool success, string error) : base(success, error)
        {
            Value = value;
        }
    }
}
