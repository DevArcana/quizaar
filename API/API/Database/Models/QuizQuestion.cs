using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Database.Models
{
    public class QuizQuestion
    {
        public long Id { get; set; }
        public string Content { get; set; }

        [Required]
        public virtual QuizTemplate Quiz { get; set; }

        public virtual ICollection<QuizAnswer> Answers { get; set; }
    }

    public class QuizQuestionDTO
    {
        public long Id { get; set; }
        public string Content { get; set; }

        public IEnumerable<QuizAnswerDTO> Answers { get; set; }

        public QuizQuestionDTO(QuizQuestion quizQuestion)
        {
            Id = quizQuestion.Id;
            Content = quizQuestion.Content;
            Answers = quizQuestion.Answers.Select(x => new QuizAnswerDTO(x));
        }
    }
}
