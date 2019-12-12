using System;
using System.Collections.Generic;
using System.Linq;
using API.Database;
using API.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    public class CategoriesController : Controller
    {
        protected readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<CategoryShallowDTO>> Get()
        {
            return Ok(_context.Categories
                .Include(c => c.Questions)
                .Select(c => new CategoryShallowDTO(c)));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<CategoryDTO> Get(long id)
        {
            return Ok(_context.Categories
                .Where(x => x.Id == id)
                .Include(c => c.Questions)
                .Select(c => new CategoryDTO(c))
                .FirstOrDefault());
        }

        // GET api/<controller>/5
        [HttpGet("{id}/generate")]
        public ActionResult<CreateQuizForm> GetQuizTemplate(long id)
        {
            var category = _context.Categories
                .Where(x => x.Id == id)
                .Include(c => c.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefault();

            if (category == null) return NoContent();

            var questions = 6;
            var answers = 4;

            var rnd = new Random();

            var quizForm = new CreateQuizForm
            {
                Name = "Example Quiz",
                Questions = category.Questions
                    .OrderBy(x => rnd.Next())
                    .Take(questions)
                    .Select(x => new CreateQuizForm.Question
                    {
                        Id = x.Id,
                        Answers = x.Answers
                            .Where(x => !x.IsCorrect)
                            .OrderBy(x => rnd.Next())
                            .Take(answers)
                            .Append(x.Answers.OrderBy(x => rnd.Next()).Where(x => x.IsCorrect).FirstOrDefault())
                            .Select(x => x.Id)
                            .ToArray()

                    })
                    .ToArray()
            };

            return Ok(null);
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<CategoryShallowDTO> Post([FromBody]CreateCategoryForm category)
        {
            if (category.Name == null || category.Name == "") return BadRequest();

            var cat = new Category
            {
                Name = category.Name
            };

            _context.Categories.Add(cat);
            _context.SaveChanges();

            return Ok(new CategoryShallowDTO(cat));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult<CategoryShallowDTO> Put(long id, [FromBody]CreateCategoryForm category)
        {
            if (category.Name == null || category.Name == "") return BadRequest();

            var cat = _context.Categories.Where(x => x.Id == id).FirstOrDefault();

            if (cat != null)
            {
                cat.Name = category.Name;
                _context.Categories.Update(cat);
                _context.SaveChanges();

                return Ok(new CategoryShallowDTO(cat));
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
            var category = _context.Categories
                .Include(c => c.Questions)
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (category != null)
            {
                if (category.Questions != null && category.Questions.Count() != 0 && !force)
                {
                    // TODO: Figure out better status codes
                    return BadRequest();
                }

                _context.Categories.Remove(category);
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
