using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Database.Models
{
    public class Question
    {
        public long Id { get; set; }
        public string Content { get; set; }
        
        [Required]
        public virtual Category Category { get; set; }
        
        public virtual IEnumerable<Answer> Answers { get; set; }
    }

    public class QuestionShallowDTO
    {
        public long Id { get; set; }
        public string Content { get; set; }

        public CategoryShallowDTO Category { get; set; }

        public QuestionShallowDTO(Question question)
        {
            Id = question.Id;
            Content = question.Content;

            Category = new CategoryShallowDTO(question.Category);
        }
    }

    public class QuestionDTO : QuestionShallowDTO
    {
        public class AnswerWrapper
        {
            public long Id { get; set; }
            public string Content { get; set; }
            public bool IsCorrect { get; set; }

            public AnswerWrapper(Answer answer)
            {
                Id = answer.Id;
                Content = answer.Content;
                IsCorrect = answer.IsCorrect;
            }
        }

        public IEnumerable<AnswerWrapper> Answers { get; set; }

        public QuestionDTO(Question question) : base(question)
        {
            Id = question.Id;
            Content = question.Content;

            Category = new CategoryShallowDTO(question.Category);

            Answers = question.Answers.Select(a => new AnswerWrapper(a));
        }
    }

    public class CreateQuestionForm
    {
        public string Content { get; set; }
        public long CategoryId { get; set; }
    }
}
