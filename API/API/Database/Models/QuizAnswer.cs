using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Database.Models
{
    public class QuizAnswer
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; }

        [Required]
        public QuizQuestion Question { get; set; }
    }
}
