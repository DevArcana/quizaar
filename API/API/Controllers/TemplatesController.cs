using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Database;
using API.Database.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    public class TemplatesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IQuizTemplateManager _quizTemplateManager;
        private readonly IQuizInstanceManager _quizInstanceManager;

        public TemplatesController(AppDbContext context, IQuizTemplateManager quizTemplateManager, IQuizInstanceManager quizInstanceManager)
        {
            _context = context;
            _quizTemplateManager = quizTemplateManager;
            _quizInstanceManager = quizInstanceManager;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<QuizTemplateShallowResponse>> Get()
        {
            return Ok(_context.QuizTemplates.Select(x => new QuizTemplateShallowResponse(x)));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<QuizTemplateResponse> Get(long id)
        {
            return Ok(_context.QuizTemplates
                .Include(x => x.Questions).ThenInclude(x => x.CorrectAnswer)
                .Include(x => x.Questions).ThenInclude(x => x.WrongAnswers)
                .Include(x => x.Questions).ThenInclude(x => x.Question)
                .Where(x => x.Id == id)
                .Select(x => new QuizTemplateResponse(x))
                .FirstOrDefault());
        }

        // GET api/<controller>/5
        [HttpGet("{id}/activate")]
        public ActionResult<QuizInstanceResponse> ActivateInstance(long id, int hours = 1, int minutes = 0, int seconds = 0)
        {
            var instance = _quizInstanceManager.ActivateQuiz(id, new TimeSpan(hours, minutes, seconds));
            return Ok(instance);
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult Post([FromBody]QuizTemplateRequestForm form)
        {
            return Ok(_quizTemplateManager.CreateQuizTemplate(form));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            // TODO: Implement this
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            _quizTemplateManager.DeleteQuizTemplate(id);
        }
    }
}
