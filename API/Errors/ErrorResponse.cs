using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ErrorResponse
    {
        public string Title { get; }
        public string Message { get; }

        public ErrorResponse(string title, string message)
        {
            Title = title;
            Message = message;
        }
    }
}
