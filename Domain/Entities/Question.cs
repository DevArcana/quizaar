using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Common;

namespace Domain.Entities
{
    public class Question : BaseEntity
    {
        public string Content { get; set; }
        public bool IsOpen { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }

        public IEnumerable<Answer> CorrectAnswers => Answers.Where(x => x.IsCorrect);
        public IEnumerable<Answer> IncorrectAnswers => Answers.Where(x => !x.IsCorrect);

        public virtual Category Category { get; set; }

        private Question()
        {
            // Needed by EF Core
        }

        public Question(bool isOpen, string question)
        {
            Content = question;
            IsOpen = isOpen;
        }
    }
}
