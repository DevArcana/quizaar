using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Database.Models.Base;

namespace API.Database.Models
{
    public class InstanceQuestion : BaseModel
    {
        public string Content { get; set; }
        public IEnumerable<InstanceAnswer> Answers { get; set; }

        public InstanceQuestion()
        {

        }

        public InstanceQuestion(TemplateQuestion question)
        {
            Content = question.Question.Content;
            Answers = question.Answers.Select(x => new InstanceAnswer(x)).OrderBy(x => Guid.NewGuid()).ToList();
        }
    }
}
