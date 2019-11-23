﻿using System;
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
    public class QuestionsController : Controller
    {
        protected readonly AppDbContext _context;

        public QuestionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<IQuestionDTO>> Get(bool shallow)
        {
            if (shallow)
                return Ok(_context.Questions
                    .Include(q => q.Category)
                    .Select(q => new QuestionShallowDTO(q)));
            else
                return Ok(_context.Questions
                    .Include(q => q.Answers)
                    .Include(q => q.Category)
                    .Select(q => new QuestionDTO(q)));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<IQuestionDTO> Get(long id, bool shallow)
        {
            if (shallow)
                return Ok(_context.Questions
                    .Include(q => q.Category)
                    .Select(q => new QuestionShallowDTO(q))
                    .Where(x => x.Id == id)
                    .FirstOrDefault());
            else
                return Ok(_context.Questions
                    .Include(q => q.Answers)
                    .Include(q => q.Category)
                    .Select(q => new QuestionDTO(q))
                    .Where(x => x.Id == id)
                    .FirstOrDefault());
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult Post([FromBody]CreateQuestionForm question)
        {
            if (question.Content == null || question.Content == "") return BadRequest();

            var category = _context.Categories.Where(x => x.Id == question.CategoryId).FirstOrDefault();

            if (category == null)
            {
                return BadRequest();
            }

            var quest = new Question
            {
                Category = category,
                Content = question.Content
            };

            _context.Questions.Add(quest);
            _context.SaveChanges();

            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult Put(long id, [FromBody]CreateQuestionForm question)
        {
            if (question.Content == null || question.Content == "") return BadRequest();

            var quest = _context.Questions.Where(x => x.Id == id).FirstOrDefault();

            if (quest != null)
            {
                var category = _context.Categories.Where(x => x.Id == question.CategoryId).FirstOrDefault();
                
                if (category == null)
                {
                    return BadRequest();
                }

                quest.Category = category;
                quest.Content = question.Content;
                _context.Questions.Update(quest);
                _context.SaveChanges();

                return Ok();
            }
            else
            {
                return NoContent();
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(long id, bool force)
        {
            var question = _context.Questions
                .Include(q => q.Answers)
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (question != null)
            {
                if (question.Answers != null && question.Answers.Count() != 0 && !force)
                {
                    return BadRequest();
                }

                _context.Questions.Remove(question);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return NoContent();
            }
        }
    }
}