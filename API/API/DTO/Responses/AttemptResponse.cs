using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Database.Models;

namespace API.DTO.Responses
{
    public class AttemptResponse
    {
        public string Identity { get; set; }
        public int PointsScored { get; set; }
        public int PointsTotal { get; set; }

        public AttemptResponse(Attempt attempt)
        {
            Identity = attempt.Identity;
            PointsScored = attempt.PointsScored;
            PointsTotal = attempt.Instance.Questions.Count();
        }
    }
}
