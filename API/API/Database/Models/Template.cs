using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Database.Models.Base;

namespace API.Database.Models
{
    public class Template : BaseModel
    {
        public string Name { get; set; }
        public virtual ICollection<TemplateQuestion> Questions { get; set; }
    }
}
