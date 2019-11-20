using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Database;
using API.Database.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class QuestionsController : Controller
    {
        protected readonly AppDbContext _context;

        public QuestionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Question> Get()
        {
            return _context.Questions;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Question Get(long id)
        {
            return _context.Questions.Where(x => x.Id == id).FirstOrDefault();
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Question question)
        {
            var quest = new Question
            {
                Category = question.Category,
                Content = question.Content
            };

            _context.Questions.Add(quest);
            _context.SaveChanges();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(long id, [FromBody]Question question)
        {
            var quest = _context.Questions.Where(x => x.Id == id).FirstOrDefault();

            if (quest != null)
            {
                quest.Category = question.Category;
                quest.Content = question.Content;
                _context.Questions.Update(quest);
                _context.SaveChanges();
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var question = _context.Questions.Where(x => x.Id == id).FirstOrDefault();

            if (question != null)
            {
                _context.Questions.Remove(question);
                _context.SaveChanges();
            }
        }
    }
}
