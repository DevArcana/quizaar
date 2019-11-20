using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Database.Models
{
    public class Quiz
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<QuizQuestion> Questions { get; set; }
    }
}
