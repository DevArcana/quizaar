using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Database;
using API.Database.Models;
using API.DTO.Forms;
using API.Utility;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public interface IInstanceService
    {
        Result<Instance> CreateInstance(long templateId, TimeSpan duration);
        Result<Instance> GetInstance(long instanceId, bool allowHistoric);

        IEnumerable<Instance> GetActiveInstances();
        IEnumerable<Instance> GetAllInstances();

        Result<Attempt> SolveQuiz(long instanceId, AttemptForm form);
    }

    public class InstanceService : IInstanceService
    {
        private readonly AppDbContext _context;

        public InstanceService(AppDbContext context)
        {
            _context = context;
        }

        public Result<Instance> CreateInstance(long templateId, TimeSpan duration)
        {
            var template = _context.Templates
                .Include(x => x.Questions).ThenInclude(x => x.Question)
                .Include(x => x.Questions).ThenInclude(x => x.Answers).ThenInclude(x => x.Answer)
                .FirstOrDefault(x => x.Id == templateId);

            if (template == null) return Result.Fail<Instance>("Couldn't find template of id " + templateId);

            var instance = new Instance(template);
            instance.EndTime = instance.StartTime.Add(duration);

            _context.Instances.Add(instance);
            _context.SaveChanges();

            return Result.Ok<Instance>(instance);
        }

        public Result<Instance> GetInstance(long instanceId, bool allowHistoric)
        {
            var instance = _context.Instances
                .Include(x => x.Questions)
                .ThenInclude(x => x.Answers)
                .Include(x => x.Attempts)
                .FirstOrDefault(x => x.Id == instanceId);

            if (instance == null) return Result.Fail<Instance>("Couldn't find instance of id " + instanceId);
            if (!instance.IsActive && !allowHistoric) return Result.Fail<Instance>("Couldn't find active instance of id " + instanceId);

            return Result.Ok<Instance>(instance);
        }

        public IEnumerable<Instance> GetActiveInstances()
        {
            return _context.Instances
                .Include(x => x.Questions)
                .ThenInclude(x => x.Answers)
                .Include(x => x.Attempts)
                .ToList()
                .Where(x => x.IsActive);
        }

        public IEnumerable<Instance> GetAllInstances()
        {
            return _context.Instances
                .Include(x => x.Questions)
                .ThenInclude(x => x.Answers)
                .Include(x => x.Attempts);
        }

        public Result<Attempt> SolveQuiz(long instanceId, AttemptForm form)
        {
            var instance = _context.Instances
                .Include(x => x.Attempts)
                .Include(x => x.Questions)
                .ThenInclude(x => x.Answers)
                .FirstOrDefault(x => x.Id == instanceId);

            if (instance == null)
                return Result.Fail<Attempt>("Couldn't find instance of id " + instanceId);
            if (!instance.IsActive)
                return Result.Fail<Attempt>("Can't solve an expired instance.");
            if (string.IsNullOrWhiteSpace(form.Identity))
                return Result.Fail<Attempt>("Invalid identity. Must not be null or whitespace only.");
            if (instance.Attempts.FirstOrDefault(x => x.Identity.Equals(form.Identity)) != null)
                return Result.Fail<Attempt>("You can only solve a quiz once.");

            var responses = new List<Response>();

            var points = 0;

            foreach (var pair in form.QuestionAnswerPairs)
            {
                var question = instance.Questions.FirstOrDefault(x => x.Id == pair.Q);
                var answer = question?.Answers.FirstOrDefault(x => x.Id == pair.A);

                if (answer == null) continue;
                if (!answer.IsCorrect) continue;

                points++;
                responses.Add(new Response
                {
                    Answer = answer
                });
            }

            var attempt = new Attempt
            {
                Identity = form.Identity,
                PointsScored = points,
                Responses = responses
            };

            instance.Attempts.Add(attempt);
            _context.Instances.Update(instance);
            _context.SaveChanges();

            return Result.Ok(attempt);
        }
    }
}
