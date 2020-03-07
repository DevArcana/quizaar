using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;

namespace Domain.Entities
{
    public class Answer : BaseEntity
    {
        public bool IsCorrect { get; set; }
        public string Content { get; }

        public virtual Question Question { get; set; }
        public long QuestionId { get; set; }

        private Answer()
        {
            // Needed by EF Core
        }

        public Answer(bool isCorrect, string answer)
        {
            IsCorrect = isCorrect;
            Content = answer;
        }
    }
}
