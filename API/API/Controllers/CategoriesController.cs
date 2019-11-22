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
    public class CategoriesController : Controller
    {
        protected readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<ICategoryDTO>> Get(bool shallow)
        {
            if (shallow)
                return Ok(_context.Categories.Select(c => new CategoryShallowDTO(c)));
            else
                return Ok(_context.Categories.Include(c => c.Questions).Select(c => new CategoryDTO(c)));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<ICategoryDTO> Get(long id, bool shallow)
        {
            if (shallow)
                return Ok(_context.Categories
                    .Where(x => x.Id == id)
                    .Select(c => new CategoryShallowDTO(c))
                    .FirstOrDefault());
            else
                return Ok(_context.Categories
                    .Where(x => x.Id == id)
                    .Include(c => c.Questions)
                    .Select(c => new CategoryDTO(c))
                    .FirstOrDefault());
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult Post([FromBody]CategoryShallowDTO category)
        {
            if (category.Name == null) return BadRequest();

            var cat = new Category
            {
                Name = category.Name
            };

            _context.Categories.Add(cat);
            _context.SaveChanges();

            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult Put(long id, [FromBody]CategoryShallowDTO category)
        {
            if (category.Name == null) return BadRequest();

            var cat = _context.Categories.Where(x => x.Id == id).FirstOrDefault();

            if (cat != null)
            {
                cat.Name = category.Name;
                _context.Categories.Update(cat);
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
        public ActionResult Delete(long id)
        {
            var category = _context.Categories.Where(x => x.Id == id).FirstOrDefault();

            if (category != null)
            {
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
