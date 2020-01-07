using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Database.Models.Base;

namespace API.Database.Models
{
    public class Instance : BaseModel
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public bool IsActive => DateTime.UtcNow < EndTime;

        public IEnumerable<InstanceQuestion> Questions { get; set; }

        public virtual IEnumerable<Attempt> Attempts { get; set; }

        public Instance()
        {

        }

        public Instance(Template template)
        {
            Name = template.Name;
            StartTime = DateTime.UtcNow;
            EndTime = DateTime.UtcNow.AddHours(1);

            Questions = template.Questions.Select(x => new InstanceQuestion(x)).OrderBy(x => Guid.NewGuid()).ToList();
        }
    }
}
