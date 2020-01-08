using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO.Forms
{
    public class AttemptForm
    {
        public string Identity { get; set; }

        public class QuestionAnswerPair
        {
            public long Q { get; set; }
            public long A { get; set; }
        }

        public IEnumerable<QuestionAnswerPair> QuestionAnswerPairs { get; set; }
    }
}
