using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Database.Models
{
    public class QuizTemplate
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<QuizQuestion> Questions { get; set; }
    }

    public class QuizTemplateShallowDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public QuizTemplateShallowDTO(QuizTemplate quizTemplate)
        {
            Id = quizTemplate.Id;
            Name = quizTemplate.Name;
        }
    }

    public class QuizTemplateDTO : QuizTemplateShallowDTO
    {
        public IEnumerable<QuizQuestionDTO> Questions { get; set; }

        public QuizTemplateDTO(QuizTemplate quizTemplate) : base(quizTemplate)
        {
            Questions = quizTemplate.Questions.Select(x => new QuizQuestionDTO(x));
        }
    }

    public class CreateQuizForm
    {
        public class Question
        {
            public long Id { get; set; }
            public ICollection<long> Answers { get; set; }
        }

        public string Name { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
