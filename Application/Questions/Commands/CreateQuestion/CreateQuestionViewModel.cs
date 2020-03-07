using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Questions.Commands.CreateQuestion
{
    public class CreateQuestionViewModel : IMapFrom<Question>
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public bool IsOpen { get; set; }
    }
}
