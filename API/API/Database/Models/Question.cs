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
        public long CategoryId { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}
