using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Application.Forms;
using API.Database;
using API.Database.Models;
using API.Utility;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public interface ITemplateService
    {
        Result<TemplateForm> GenerateForm(long categoryId, int questionsCount, int answersPerQuestion);
        Result<Template> CreateTemplate(TemplateForm form);
        Result<Template> ModifyTemplate(long templateId, TemplateForm form);
        Result<Template> DeleteTemplate(long templateId);
        Result<Template> GetTemplate(long templateId);
        IEnumerable<Template> GetTemplates();
    }

    public class TemplateService : ITemplateService
    {
        private readonly AppDbContext _context;

        public TemplateService(AppDbContext context)
        {
            _context = context;
        }

        public Result<TemplateForm> GenerateForm(long categoryId, int questionsCount, int answersPerQuestion)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == categoryId);

            if (category == null) return Result.Fail<TemplateForm>("Couldn't find category of id " + categoryId);

            throw new NotImplementedException();
        }

        public Result<Template> CreateTemplate(TemplateForm form)
        {
            if (string.IsNullOrEmpty(form.Name)) return Result.Fail<Template>("Name can't be null or empty.");

            var questions = new List<TemplateQuestion>();

            foreach (var questionForm in form.Questions)
            {
                var question = _context.Questions
                    .Include(x => x.Answers)
                    .FirstOrDefault(x => x.Id == questionForm.Id);

                if (question == null) return Result.Fail<Template>("Invalid question id " + questionForm.Id);

                var answers = question.Answers
                    .Where(x => questionForm.Answers.Contains(x.Id))
                    .Select(x => new TemplateAnswer { Answer = x })
                    .ToList();

                questions.Add(new TemplateQuestion
                {
                    Question = question,
                    Answers = answers
                });
            }

            var template = new Template
            {
                Name = form.Name,
                Questions = questions
            };

            _context.Templates.Add(template);
            _context.SaveChanges();

            return Result.Ok(template);
        }

        public Result<Template> ModifyTemplate(long templateId, TemplateForm form)
        {
            throw new NotImplementedException();
        }

        public Result<Template> DeleteTemplate(long templateId)
        {
            throw new NotImplementedException();
        }

        public Result<Template> GetTemplate(long templateId)
        {
            var template = _context.Templates
                .Include(x => x.Questions).ThenInclude(x => x.Question)
                .Include(x => x.Questions).ThenInclude(x => x.Answers).ThenInclude(x => x.Answer)
                .FirstOrDefault(x => x.Id == templateId);

            return template == null ? Result.Fail<Template>("Couldn't find template of id " + templateId) : Result.Ok(template);
        }

        public IEnumerable<Template> GetTemplates()
        {
            return _context.Templates
                .Include(x => x.Questions).ThenInclude(x => x.Question)
                .Include(x => x.Questions).ThenInclude(x => x.Answers).ThenInclude(x => x.Answer);
        }
    }
}
