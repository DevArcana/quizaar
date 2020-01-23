using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Subcategory
    {
        public long Id { get; set; }
        public string Name { get; set; }

        private readonly List<QuestionSet> _questionSets = new List<QuestionSet>();
        public IReadOnlyCollection<QuestionSet> QuestionSets => _questionSets.AsReadOnly();

        private Subcategory()
        {

        }

        public Subcategory(string name, List<QuestionSet> questionSets)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Subcategory name can not be null, empty or whitespace.", nameof(name));

            Name = name;
            _questionSets = questionSets ?? throw new ArgumentNullException(nameof(questionSets), "You must provide a list of question sets, it can be empty!");
        }
    }
}
