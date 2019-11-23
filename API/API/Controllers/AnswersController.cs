using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Database;
using API.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    public class AnswersController : Controller
    {
        protected readonly AppDbContext _context;

        public AnswersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<AnswerDTO>> Get()
        {
            return Ok(_context.Answers
                .Include(a => a.Question)
                .Select(a => new AnswerDTO(a)));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<AnswerDTO> Get(long id)
        {
            return Ok(_context.Answers
                .Include(a => a.Question)
                .Where(a => a.Id == id)
                .Select(a => new AnswerDTO(a))
                .FirstOrDefault());
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult Post([FromBody]CreateAnswerForm answerForm)
        {
            if (answerForm == null) return BadRequest();
            if (answerForm.Content == null || answerForm.Content == "") return BadRequest();

            var question = _context.Questions.Where(q => q.Id == answerForm.QuestionId).FirstOrDefault();

            if (question == null) return BadRequest();

            var answer = new Answer
            {
                Content = answerForm.Content,
                IsCorrect = answerForm.IsCorrect,
                Question = question
            };

            _context.Answers.Add(answer);
            _context.SaveChanges();

            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult Put(long id, [FromBody]CreateAnswerForm answerForm)
        {
            if (answerForm == null) return BadRequest();
            if (answerForm.Content == null || answerForm.Content == "") return BadRequest();

            var answer = _context.Answers.Where(a => a.Id == id).FirstOrDefault();

            if (answer == null) return NoContent();

            var question = _context.Questions.Where(q => q.Id == answerForm.QuestionId).FirstOrDefault();

            if (question == null) return BadRequest();

            answer.Content = answerForm.Content;
            answer.IsCorrect = answerForm.IsCorrect;
            answer.Question = question;

            _context.Answers.Update(answer);
            _context.SaveChanges();

            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            var answer = _context.Answers.Where(a => a.Id == id).FirstOrDefault();
            if (answer == null) return NoContent();

            _context.Answers.Remove(answer);
            _context.SaveChanges();

            return Ok();
        }
    }
}
