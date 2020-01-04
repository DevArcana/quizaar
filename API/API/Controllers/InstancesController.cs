using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Database.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    public class InstancesController : Controller
    {
        private readonly IQuizInstanceManager _quizInstanceManager;

        public InstancesController(IQuizInstanceManager quizInstanceManager)
        {
            _quizInstanceManager = quizInstanceManager;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<QuizInstanceResponse> Get(bool includeHistoric)
        {
            return includeHistoric ? 
                _quizInstanceManager.GetAllInstances().Select(instance => new QuizInstanceResponse(instance)) :
                _quizInstanceManager.GetAllActiveInstances().Select(instance => new QuizInstanceResponse(instance));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public QuizInstanceResponse Get(long id)
        {
            return _quizInstanceManager.GetInstanceResponse(id, false);
        }

        // POST api/<controller>/5/answer
        [HttpPost("{id}/answer")]
        public ActionResult<QuizAnswerSheetResponse> Answer(long id, [FromBody] QuizAnswerSheetForm answerSheetForm)
        {
            var result = _quizInstanceManager.AddAnswerSheet(id, answerSheetForm);

            if (result.Success) return Ok(result.Value);

            return BadRequest(result.Error);
        }

        // GET api/<controller>/5/answers
        [HttpGet("{id}/answers")]
        public ActionResult<QuizAnswerSheetResponse> GetAnswers(long id)
        {
            var result = _quizInstanceManager.GetAnswerSheets(id);

            if (result.Success) return Ok(result.Value);

            return BadRequest(result.Error);
        }
    }
}
