using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Database.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public virtual IEnumerable<Question> Questions { get; set; }
    }

    public class CategoryDTO
    {
        public class QuestionWrapper
        {
            public long Id { get; set; }
            public string Content { get; set; }

            public QuestionWrapper(Question question)
            {
                Id = question.Id;
                Content = question.Content;
            }
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual IEnumerable<QuestionWrapper> Questions { get; set; }

        public CategoryDTO(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            Questions = category.Questions.Select(q => new QuestionWrapper(q));
        }
    }
}
