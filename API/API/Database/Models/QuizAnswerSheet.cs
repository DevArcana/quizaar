using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Database.Models
{
    public class QuizAnswerSheet
    {
        public long Id { get; set; }
        public string Identity { get; set; }

        public virtual QuizInstance Instance { get; set; }

        public virtual IEnumerable<QuizAnswerQuestionPair> Answers { get; set; }
    }

    public class QuizAnswerQuestionPair
    {
        public long Id { get; set; }
        public virtual QuizInstanceQuestion Question { get; set; }
        public virtual QuizInstanceAnswer Answer { get; set; }
    }

    public class QuizAnswerSheetForm
    {
        public long InstanceId { get; set; }
        public string Identity { get; set; }
        public IEnumerable<(long, long)> QuestionAnswerPairs { get; set; }
    }
}
