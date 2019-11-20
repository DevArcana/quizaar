using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Database;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/quizes")]
    public class QuizController : Controller
    {
        protected readonly AppDbContext _context;
        public QuizController(AppDbContext context) 
        {
            _context = context;
        }

        // GET: api/v1/quizes>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _context.Quizes.Select(x => x.Name);
        }

        // GET api/v1/quizes/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/v1/quizes
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/v1/quizes/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/v1/quizes/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
