using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.Database.Models.Base;

namespace API.Database.Models
{
    public class Answer : BaseModel
    {
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
        
        [Required]
        public virtual Question Question { get; set; }
    }

    public class AnswerDTO
    {
        public class QuestionWrapper
        {
            public long Id { get; set; }
            public string Content { get; set; }

            public QuestionWrapper(Question question)
            {
                Id = question.Id;
                Content = question.Content;
            }
        }

        public long Id { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; }

        public QuestionWrapper Question { get; set; }

        public AnswerDTO(Answer answer)
        {
            Id = answer.Id;
            Content = answer.Content;
            IsCorrect = answer.IsCorrect;

            Question = new QuestionWrapper(answer.Question);
        }
    }

    public class CreateAnswerForm
    {
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
        public long QuestionId { get; set; }
    }
}
