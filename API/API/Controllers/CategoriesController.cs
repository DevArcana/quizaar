using System;
using System.Collections.Generic;
using System.Linq;
using API.Database;
using API.Database.Models;
using API.DTO.Forms;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ITemplateService _templateService;

        public CategoriesController(AppDbContext context, ITemplateService templateService)
        {
            _context = context;
            _templateService = templateService;
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

        //GET api/<controller>/5
        [HttpGet("{id}/generate")]
        public ActionResult<TemplateForm> GetQuizTemplate(long id, int questionsCount, int answersPerQuestion, string quizName)
        {
            var result = _templateService.GenerateForm(id, quizName, questionsCount, answersPerQuestion);

            if (result.Failure) return BadRequest(result.Error);
            return Ok(result.Value);
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<CategoryShallowDTO> Post([FromBody]CreateCategoryForm category)
        {
            if (string.IsNullOrEmpty(category.Name)) return BadRequest();

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
            if (string.IsNullOrEmpty(category.Name)) return BadRequest();

            var cat = _context.Categories.FirstOrDefault(x => x.Id == id);

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
                .FirstOrDefault(x => x.Id == id);

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
