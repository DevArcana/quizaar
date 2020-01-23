using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Question
    {
        public long Id { get; set; }
        public string Content { get; set; }

        public bool IsOpen { get; set; }

        private readonly List<Answer> _answers = new List<Answer>();
        public IReadOnlyCollection<Answer> Answers => _answers.AsReadOnly();

        private Question()
        {

        }

        public Question(string question, List<Answer> answers)
        {
            if (string.IsNullOrWhiteSpace(question)) throw new ArgumentException("Question must not be empty, null or whitespace!", nameof(question));
            Content = question;

            _answers = answers ?? throw new ArgumentNullException(nameof(answers), "You must provide a list of answers, it can be empty!");
        }
    }
}
