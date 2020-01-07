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

        public virtual IEnumerable<InstanceQuestion> Questions { get; set; }

        public virtual IEnumerable<Attempt> Attempts { get; set; }
    }
}
