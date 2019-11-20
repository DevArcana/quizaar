using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Database.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        
        public ICollection<Question> Questions { get; set; }
    }
}
