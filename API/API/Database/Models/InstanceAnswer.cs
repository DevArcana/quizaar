using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Database.Models.Base;

namespace API.Database.Models
{
    public class InstanceAnswer : BaseModel
    {
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
    }
}
