using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.ErrorHandling
{
    public struct Error
    {
        public ErrorType Type { get; }
        public string Message { get; }

        public Error(ErrorType type, string message)
        {
            Type = type;
            Message = message;
        }

        public Error(string message) : this(ErrorType.GENERIC, message)
        {

        }
    }
}
