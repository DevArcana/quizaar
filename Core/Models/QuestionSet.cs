using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class QuestionSet
    {
        public long Id { get; set; }

        public string Name { get; set; }

        private readonly List<Question> _questions = new List<Question>();

        public IReadOnlyCollection<Question> Questions => _questions.AsReadOnly();

        private QuestionSet()
        {

        }

        public QuestionSet(string name, List<Question> questions)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Question set name can not be null, empty or whitespace.", nameof(name));

            Name = name;
            _questions = questions ?? throw new ArgumentNullException(nameof(questions), "You must provide a list of questions, it can be empty!");
        }
    }
}
