using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Database.Models;

namespace API.DTO.Responses
{
    public class InstanceResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public class Question
        {
            public long Id { get; set; }
            public string Content { get; set; }

            public class Answer
            {
                public long Id { get; set; }
                public string Content { get; set; }

                public Answer(InstanceAnswer answer)
                {
                    Id = answer.Id;
                    Content = answer.Content;
                }
            }

            public IEnumerable<Answer> Answers { get; set; }

            public Question(InstanceQuestion question)
            {
                Id = question.Id;
                Content = question.Content;
                Answers = question.Answers.Select(x => new Answer(x));
            }
        }

        public IEnumerable<Question> Questions { get; set; }

        public InstanceResponse(Instance instance)
        {
            Id = instance.Id;
            Name = instance.Name;
            StartTime = instance.StartTime;
            EndTime = instance.EndTime;
            Questions = instance.Questions.Select(x => new Question(x));
        }
    }
}
