using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Extensions.WebEncoders.Testing;

namespace API.Application.Forms
{
    public class TemplateForm
    {
        public string Name { get; set; }

        public class Question
        {
            public long Id { get; set; }
            public IEnumerable<long> Answers { get; set; }
        }

        public IEnumerable<Question> Questions { get; set; }
    }
}
