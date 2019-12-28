using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Database.Models
{
    public class QuizInstance
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<QuizInstanceQuestion> Questions { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime ExpireTime { get; set; }

        public bool IsOpen => ExpireTime > DateTime.UtcNow;

        public ICollection<QuizAnswerSheet> AnswerSheets { get; set; }
    }

    public class QuizInstanceQuestion
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public IEnumerable<QuizInstanceAnswer> WrongAnswers { get; set; }
        public IEnumerable<QuizInstanceAnswer> CorrectAnswers { get; set; }
    }

    public class QuizInstanceAnswer
    {
        public long Id { get; set; }
        public long MaskingId { get; set; }
        public string Content { get; set; }
    }

    public class QuizInstanceResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<QuizInstanceQuestionResponse> Questions { get; set; }

        public QuizInstanceResponse(QuizInstance instance)
        {
            Id = instance.Id;
            Name = instance.Name;

            Questions = instance.Questions.Select(x => new QuizInstanceQuestionResponse(x)).ToList();
        }
    }

    public class QuizInstanceQuestionResponse
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public IEnumerable<QuizInstanceAnswerResponse> Answers { get; set; }

        public QuizInstanceQuestionResponse(QuizInstanceQuestion question)
        {
            Id = question.Id;
            Content = question.Content;
            Answers = question.CorrectAnswers
                .Concat(question.WrongAnswers)
                .OrderBy(x => Guid.NewGuid())
                .Select(x => new QuizInstanceAnswerResponse(x))
                .ToList();
        }
    }

    public class QuizInstanceAnswerResponse
    {
        public long Id { get; set; }
        public string Content { get; set; }

        public QuizInstanceAnswerResponse(QuizInstanceAnswer answer)
        {
            Id = answer.MaskingId;
            Content = answer.Content;
        }
    }
}
