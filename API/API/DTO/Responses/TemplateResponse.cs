using System.Collections.Generic;
using System.Linq;
using API.Database.Models;

namespace API.DTO.Responses
{
    public class TemplateResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public class Question
        {
            public long Id { get; set; }
            public string Content { get; set; }

            public class Answer
            {
                public long Id { get; set; }
                public bool IsCorrect { get; set; }
                public string Content { get; set; }

                public Answer(TemplateAnswer answer)
                {
                    Id = answer.Answer.Id;
                    IsCorrect = answer.Answer.IsCorrect;
                    Content = answer.Answer.Content;
                }
            }

            public IEnumerable<Answer> Answers { get; set; }

            public Question(TemplateQuestion question)
            {
                Id = question.Question.Id;
                Content = question.Question.Content;
                Answers = question.Answers.Select(x => new Answer(x));
            }
        }

        public IEnumerable<Question> Questions { get; set; }

        public TemplateResponse(Template template)
        {
            Id = template.Id;
            Name = template.Name;
            Questions = template.Questions.Select(x => new Question(x));
        }
    }
}
