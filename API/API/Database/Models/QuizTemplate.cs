using System.Collections.Generic;
using System.Linq;

namespace API.Database.Models
{
    public class QuizTemplate
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public virtual IEnumerable<QuizTemplateQuestion> Questions { get; set; }
    }

    public class QuizTemplateQuestion
    {
        public long Id { get; set; }

        public Question Question { get; set; }
        
        public Answer CorrectAnswer { get; set; }
        public IEnumerable<Answer> WrongAnswers { get; set; }
    }

    public class QuizTemplateQuestionResponse
    {
        public long Id { get; set; }
        public string Content { get; set; }

        public class AnswerResponse
        {
            public long Id { get; set; }
            public string Content { get; set; }

            public AnswerResponse(Answer answer)
            {
                Id = answer.Id;
                Content = answer.Content;
            }
        }

        public AnswerResponse CorrectAnswer { get; set; }
        public IEnumerable<AnswerResponse> WrongAnswers { get; set; }

        public QuizTemplateQuestionResponse(QuizTemplateQuestion question)
        {
            Id = question.Id;
            Content = question.Question.Content;

            CorrectAnswer = new AnswerResponse(question.CorrectAnswer);
            WrongAnswers = question.WrongAnswers.Select(x => new AnswerResponse(x));
        }
    }

    public class QuizTemplateShallowResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public QuizTemplateShallowResponse(QuizTemplate template)
        {
            Id = template.Id;
            Name = template.Name;
        }
    }

    public class QuizTemplateResponse : QuizTemplateShallowResponse
    {
        public IEnumerable<QuizTemplateQuestionResponse> Questions { get; set; }

        public QuizTemplateResponse(QuizTemplate template) : base(template)
        {
            Questions = template.Questions.Select(x => new QuizTemplateQuestionResponse(x));
        }
    }

    public class QuizTemplateRequestForm
    {
        public string Name { get; set; }

        public class QuestionWrapper
        {
            public long Id { get; set; }
            public long CorrectAnswerId { get; set; }
            public IEnumerable<long> WrongAnswersIds { get; set; }
        }

        public IEnumerable<QuestionWrapper> Questions { get; set; }
    }
}
