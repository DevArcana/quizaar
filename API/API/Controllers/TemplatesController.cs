using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Database.Models;
using API.DTO.Forms;
using API.DTO.Responses;
using API.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    public class TemplatesController : Controller
    {
        private readonly ITemplateService _service;

        public TemplatesController(ITemplateService service)
        {
            _service = service;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<TemplateResponse> Get()
        {
            var result = _service.GetTemplates().Select(x => new TemplateResponse(x));

            return result;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var result = _service.GetTemplate(id);

            if (result.Success) return Ok(new TemplateResponse(result.Value));

            return BadRequest(result.Error);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]TemplateForm form)
        {
            var result = _service.CreateTemplate(form);

            if (result.Success) return Ok(new TemplateResponse(result.Value));

            return BadRequest(result.Error);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody]TemplateForm form)
        {
            var result = _service.ModifyTemplate(id, form);

            if (result.Success) return Ok();

            return BadRequest(result.Error);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var result = _service.DeleteTemplate(id);

            if (result.Success) return Ok(result.Value);

            return BadRequest(result.Error);
        }
    }
}
