using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class OperationError : ErrorResponse
    {
        public OperationError(string message) : base("Operation Error", message)
        {
        }
    }
}
