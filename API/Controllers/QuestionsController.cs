using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.ViewModels;
using Application.Questions.Commands.CreateQuestion;
using Application.Questions.Queries.ListQuestions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/categories")]
    public class QuestionsController : ApiController
    {
        [HttpGet("{categoryId}/questions")]
        public async Task<ActionResult<PaginatedList<QuestionDto>>> ListQuestions(long categoryId, [FromQuery] ListQuestionsQuery query)
        {
            query.CategoryId = categoryId;
            return await ExecuteCommand(query);
        }

        [HttpPost("{categoryId}/questions")]
        public async Task<ActionResult<CreateQuestionViewModel>> CreateQuestion(long categoryId, [FromQuery] CreateQuestionCommand command)
        {
            command.CategoryId = categoryId;
            return await ExecuteCommand(command);
        }
    }
}
