using System.Collections.Generic;

namespace API.DTO.Forms
{
    public class TemplateForm
    {
        public string Name { get; set; }

        public class Question
        {
            public long Id { get; set; }
            public IEnumerable<long> Answers { get; set; }
        }

        public IEnumerable<Question> Questions { get; set; }
    }
}
