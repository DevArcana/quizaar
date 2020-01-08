using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Database.Models.Base;

namespace API.Database.Models
{
    public class Attempt : BaseModel
    {
        public string Identity { get; set; }

        public int PointsScored { get; set; }

        public Instance Instance { get; set; }

        public virtual IEnumerable<Response> Responses { get; set; }
    }
}
