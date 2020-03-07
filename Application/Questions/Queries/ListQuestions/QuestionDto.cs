using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Questions.Queries.ListQuestions
{
    public class QuestionDto : IMapFrom<Question>
    {
        public long Id { get; set; }
        public bool IsOpen { get; set; }
        public string Content { get; set; }
    }
}
