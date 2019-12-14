using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Database;
using API.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public interface IQuizService
    {
        CreateQuizForm GenerateQuizFromCategory(long categoryId, int questionsCount, int answersPerQuestion, string quizName);
    }

    public class QuizService : IQuizService
    {
        private readonly AppDbContext _context;

        public QuizService(AppDbContext context)
        {
            _context = context;
        }

        private CreateQuizForm.Question[] ShuffleQuestions(Category category, int questionsCount, int answersPerQuestions)
        {
            var questions = _context.Questions.Where(x =>
                x.Answers.Count(x => x.IsCorrect) >= 1 &&
                x.Answers.Count(x => !x.IsCorrect) >= answersPerQuestions - 1);

            if (questions.Count() < questionsCount) return null;

            var result = questions.ToList().OrderBy(x => Guid.NewGuid()).Take(questionsCount).Select(x =>
            {
                var goodAnswer = x.Answers
                    .Where(x => x.IsCorrect)
                    .OrderBy(x => Guid.NewGuid())
                    .Select(x => x.Id)
                    .FirstOrDefault();
                
                var badAnswers = x.Answers
                    .Where(x => !x.IsCorrect)
                    .OrderBy(x => Guid.NewGuid())
                    .Select(x => x.Id)
                    .Take(answersPerQuestions - 1);

                return new CreateQuizForm.Question
                {
                    Id = x.Id,
                    Answers = badAnswers.Append(goodAnswer).OrderBy(x => Guid.NewGuid()).ToArray()
                };
            }).ToArray();

            return result;
        }

        public CreateQuizForm GenerateQuizFromCategory(long categoryId, int questionsCount, int answersPerQuestion, string quizName)
        {
            var category = _context.Categories
                .Include(x => x.Questions)
                .ThenInclude(x => x.Answers)
                .FirstOrDefault(x => x.Id == categoryId);

            // error: category not found
            if (category == null) return null;

            var questions = ShuffleQuestions(category, questionsCount, answersPerQuestion);

            // error: not enough questions in db fitting answer count
            if (questions == null) return null;

            var form = new CreateQuizForm
            {
                Name = quizName,
                Questions = questions
            };

            return form;
        }
    }
}
