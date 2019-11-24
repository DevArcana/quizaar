using System;
using System.Collections.Generic;
using System.Text;

namespace QuizaarDesktopUI.Library.Models
{
    public class CategoryDTO : ICategoryDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        // public IEnumerable<QuestionShallowDTO> Questions { get; set; }
    }
}
