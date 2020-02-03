using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ValidationError : ErrorResponse
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationError(IDictionary<string, string[]> errors) : base("Validation Error", "One or more validation errors have occurred.")
        {
            Errors = errors;
        }
    }
}
