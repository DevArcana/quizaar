using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO.Responses;
using API.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    public class InstancesController : Controller
    {
        private readonly IInstanceService _instanceService;

        public InstancesController(IInstanceService instanceService)
        {
            _instanceService = instanceService;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<InstanceResponse> Get()
        {
            return _instanceService.GetActiveInstances().Select(x => new InstanceResponse(x));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var result = _instanceService.GetInstance(id, false);

            if (result.Success)
            {
                return Ok(new InstanceResponse(result.Value));
            }

            return BadRequest(result.Error);
        }
    }
}
