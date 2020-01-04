using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using API.Database;
using API.Database.Models;
using API.Utility;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public interface IQuizInstanceManager
    {
        QuizInstance ActivateQuiz(long quizId, TimeSpan duration);
        QuizInstance GetInstance(long instanceId, bool allowHistoric);
        QuizInstanceResponse GetInstanceResponse(long instanceId, bool allowHistoric);
        
        IEnumerable<QuizInstance> GetAllInstances();
        IEnumerable<QuizInstance> GetAllActiveInstances();
        IEnumerable<QuizInstance> GetAllHistoricInstances();

        bool DeleteInstance(long instanceId);

        Result<QuizAnswerSheetResponse> AddAnswerSheet(long instanceId, QuizAnswerSheetForm form);
        Result<IEnumerable<QuizAnswerSheetResponse>> GetAnswerSheets(long instanceId);
    }

    public class QuizInstanceManager : IQuizInstanceManager
    {
        private readonly AppDbContext _context;

        public QuizInstanceManager(AppDbContext context)
        {
            _context = context;
        }

        public QuizInstance ActivateQuiz(long quizId, TimeSpan duration)
        {
            var quiz = _context.QuizTemplates
                .Include(template => template.Questions).ThenInclude(question => question.CorrectAnswer)
                .Include(template => template.Questions).ThenInclude(question => question.WrongAnswers)
                .Include(template => template.Questions).ThenInclude(question => question.Question)
                .FirstOrDefault(template => template.Id == quizId);

            if (quiz == null) return null;

            var creationTime = DateTime.UtcNow;

            var questions = quiz.Questions.Select(question =>
            {
                var randomOrder = Enumerable.Range(1, question.WrongAnswers.Count() + 1)
                    .OrderBy(x => Guid.NewGuid())
                    .ToImmutableArray();
                var currentIndex = 0;


                return new QuizInstanceQuestion
                {
                    Content = question.Question.Content,
                    CorrectAnswers = new List<QuizInstanceAnswer>()
                    {
                        new QuizInstanceAnswer
                        {
                            Content = question.CorrectAnswer.Content,
                            MaskingId = randomOrder[currentIndex++]
                        }
                    },
                    WrongAnswers = question.WrongAnswers.Select(answer => new QuizInstanceAnswer
                    {
                        Content = answer.Content,
                        MaskingId = randomOrder[currentIndex++]
                    }).ToList()
                };
            }).ToList();

            var instance = new QuizInstance
            {
                CreateTime = creationTime,
                ExpireTime = creationTime.Add(duration),
                Name = quiz.Name,
                Questions = questions
            };

            _context.QuizInstances.Add(instance);
            _context.SaveChanges();

            return instance;
        }

        public QuizInstance GetInstance(long instanceId, bool allowHistoric)
        {
            return _context.QuizInstances
                .Include(instance => instance.Questions).ThenInclude(question => question.CorrectAnswers)
                .Include(instance => instance.Questions).ThenInclude(question => question.WrongAnswers)
                .FirstOrDefault(instance => instance.Id == instanceId);
        }

        public QuizInstanceResponse GetInstanceResponse(long instanceId, bool allowHistoric)
        {
            var instance = GetInstance(instanceId, allowHistoric);

            return instance == null ? null : new QuizInstanceResponse(instance);
        }

        public IEnumerable<QuizInstance> GetAllInstances()
        {
            return _context.QuizInstances
                .Include(instance => instance.Questions).ThenInclude(question => question.CorrectAnswers)
                .Include(instance => instance.Questions).ThenInclude(question => question.WrongAnswers);
        }

        public IEnumerable<QuizInstance> GetAllActiveInstances()
        {
            return GetAllInstances().Where(instance => instance.IsOpen);
        }

        public IEnumerable<QuizInstance> GetAllHistoricInstances()
        {
            return GetAllInstances().Where(instance => !instance.IsOpen);
        }

        public bool DeleteInstance(long instanceId)
        {
            throw new NotImplementedException();
        }

        public Result<QuizAnswerSheetResponse> AddAnswerSheet(long instanceId, QuizAnswerSheetForm form)
        {
            var instance = _context.QuizInstances
                .Include(x => x.AnswerSheets)
                .Include(x => x.Questions).ThenInclude(x => x.CorrectAnswers)
                .Include(x => x.Questions).ThenInclude(x => x.WrongAnswers)
                .FirstOrDefault(x => x.Id == instanceId);

            if (instance == null) return Result.Fail<QuizAnswerSheetResponse>("Couldn't find instance of id " + instanceId);

            if (string.IsNullOrEmpty(form.Identity)) return Result.Fail<QuizAnswerSheetResponse>("Invalid identity provided.");

            var answers = form.QuestionAnswerPairs.Select(pair =>
            {
                var question = instance.Questions.FirstOrDefault(x => x.Id == pair.Q);

                var correctAnswer = question.CorrectAnswers.FirstOrDefault(x => x.MaskingId == pair.A);

                if (correctAnswer != null)
                {
                    return new QuizAnswerQuestionPair
                    {
                        Question = question,
                        Answer = correctAnswer,
                        IsCorrect = true
                    };
                }

                var wrongAnswer = question.WrongAnswers.FirstOrDefault(x => x.MaskingId == pair.A);

                return new QuizAnswerQuestionPair
                {
                    Question = question,
                    Answer = wrongAnswer,
                    IsCorrect = false
                };
            });

            var pointsScored = answers.Count(x => x.IsCorrect);

            var answerSheet = new QuizAnswerSheet
            {
                Identity = form.Identity,
                Instance = instance,
                Answers = answers,
                PointsScored = pointsScored
            };

            instance.AnswerSheets.Add(answerSheet);

            _context.QuizInstances.Update(instance);
            _context.SaveChanges();

            return Result.Ok(new QuizAnswerSheetResponse(answerSheet));
        }

        public Result<IEnumerable<QuizAnswerSheetResponse>> GetAnswerSheets(long instanceId)
        {
            var instance = _context.QuizInstances
                .Include(x => x.AnswerSheets)
                .Include(x => x.Questions)
                .FirstOrDefault(x => x.Id == instanceId);

            if (instance == null) return Result.Fail<IEnumerable<QuizAnswerSheetResponse>>("Couldn't find instance of id " + instanceId);
            
            return Result.Ok(instance.AnswerSheets.Select(x => new QuizAnswerSheetResponse(x)));
        }
    }
}
