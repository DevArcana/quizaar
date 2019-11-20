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
        public virtual Quiz Quiz { get; set; }

        public virtual ICollection<QuizAnswer> Answers { get; set; }
    }
}
