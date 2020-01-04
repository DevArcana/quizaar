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
        public int PointsScored { get; set; }

        public virtual QuizInstance Instance { get; set; }

        public virtual IEnumerable<QuizAnswerQuestionPair> Answers { get; set; }
    }

    public class QuizAnswerQuestionPair
    {
        public long Id { get; set; }
        public bool IsCorrect { get; set; }
        public virtual QuizInstanceQuestion Question { get; set; }
        public virtual QuizInstanceAnswer Answer { get; set; }
    }

    public class QuizAnswerSheetForm
    {
        public string Identity { get; set; }

        public class QuestionAnswerPair
        {
            public long Q { get; set; }
            public long A { get; set; }
        }

        public IEnumerable<QuestionAnswerPair> QuestionAnswerPairs { get; set; }
    }

    public class QuizAnswerSheetResponse
    {
        public string Identity { get; set; }
        public int Correct { get; set; }
        public int Total { get; set; }

        public QuizAnswerSheetResponse(QuizAnswerSheet answerSheet)
        {
            Identity = answerSheet.Identity;
            Correct = answerSheet.PointsScored;
            Total = answerSheet.Instance.Questions.Count();
        }
    }
}
