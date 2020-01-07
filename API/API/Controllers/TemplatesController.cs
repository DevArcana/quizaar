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
        private readonly ITemplateService _templateService;
        private readonly IInstanceService _instanceService;

        public TemplatesController(ITemplateService templateService, IInstanceService instanceService)
        {
            _templateService = templateService;
            _instanceService = instanceService;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<TemplateResponse> Get()
        {
            var result = _templateService.GetTemplates().Select(x => new TemplateResponse(x));

            return result;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var result = _templateService.GetTemplate(id);

            if (result.Success) return Ok(new TemplateResponse(result.Value));

            return BadRequest(result.Error);
        }

        // GET api/<controller>/5/activate
        [HttpGet("{id}/activate")]
        public IActionResult Activate(long id)
        {
            var result = _instanceService.CreateInstance(id, TimeSpan.FromHours(1));

            if (result.Success) return Ok(new InstanceResponse(result.Value));

            return BadRequest(result.Error);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]TemplateForm form)
        {
            var result = _templateService.CreateTemplate(form);

            if (result.Success) return Ok(new TemplateResponse(result.Value));

            return BadRequest(result.Error);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody]TemplateForm form)
        {
            var result = _templateService.ModifyTemplate(id, form);

            if (result.Success) return Ok();

            return BadRequest(result.Error);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var result = _templateService.DeleteTemplate(id);

            if (result.Success) return Ok(result.Value);

            return BadRequest(result.Error);
        }
    }
}
