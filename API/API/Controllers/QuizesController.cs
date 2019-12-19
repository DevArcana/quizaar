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
    public class QuizesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IQuizTemplateManager _quizTemplateManager;

        public QuizesController(AppDbContext context, IQuizTemplateManager quizTemplateManager)
        {
            _context = context;
            _quizTemplateManager = quizTemplateManager;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<QuizTemplate>> Get()
        {
            return Ok(_context.Quizes
                .Include(x => x.Questions)
                .ThenInclude(x => x.Answers)
                .Select(x => new QuizTemplateDTO(x)));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<QuizTemplate> Get(long id)
        {
            return Ok(_context.Quizes
                .Include(x => x.Questions)
                .ThenInclude(x => x.Answers)
                .Select(x => new QuizTemplateDTO(x))
                .FirstOrDefault(x => x.Id == id));
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult Post([FromBody]CreateQuizForm form)
        {
            return Ok(_quizTemplateManager.CreateQuizTemplate(form));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            _quizTemplateManager.DeleteQuizTemplate(id);
        }
    }
}
