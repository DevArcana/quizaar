using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Database.Models.Base;

namespace API.Database.Models
{
    public class Response : BaseModel
    {
        public virtual InstanceAnswer Answer { get; set; }
    }
}
