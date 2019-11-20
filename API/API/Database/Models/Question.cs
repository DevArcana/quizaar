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
        public Category Category { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}
