using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Database.Models.Base;

namespace API.Database.Models
{
    public class TemplateQuestion : BaseModel
    {
        public Question Question { get; set; }
        public ICollection<TemplateAnswer> Answers { get; set; }
    }
}
