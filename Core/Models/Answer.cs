using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Answer
    {
        public long Id { get; set; }
        public string Content { get; set; }

        private Answer()
        {

        }

        public Answer(string answer)
        {
            if (string.IsNullOrWhiteSpace(answer)) throw new ArgumentException("Question must not be empty, null or whitespace!", nameof(answer));
            Content = answer;
        }
    }
}
