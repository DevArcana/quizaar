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
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        protected readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<CategoryDTO> Get()
        {
            return _context.Categories.Include(c => c.Questions).Select(c => new CategoryDTO(c));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Category Get(long id)
        {
            return _context.Categories.Where(x => x.Id == id).FirstOrDefault();
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Category category)
        {
            var cat = new Category
            {
                Name = category.Name
            };

            _context.Categories.Add(cat);
            _context.SaveChanges();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(long id, [FromBody]Category category)
        {
            var cat = _context.Categories.Where(x => x.Id == id).FirstOrDefault();

            if (category != null)
            {
                category.Name = category.Name;
                _context.Categories.Update(category);
                _context.SaveChanges();
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var category = _context.Categories.Where(x => x.Id == id).FirstOrDefault();

            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}
