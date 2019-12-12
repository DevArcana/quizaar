using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Database.Models
{
    public class Quiz
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<QuizQuestion> Questions { get; set; }
    }

    public class CreateQuizForm
    {
        public class Question
        {
            public long Id { get; set; }
            public long[] Answers { get; set; }
        }

        public string Name { get; set; }

        public Question[] Questions { get; set; }
    }
}
