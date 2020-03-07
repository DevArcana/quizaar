using System.Threading.Tasks;
using API.Errors;
using Application.Common.ErrorHandling;
using Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        private ActionResult HandleError(Error error)
        {
            if (error.Type == ErrorType.NOT_FOUND)
            {
                return NotFound(new OperationError(error.Message));
            }

            return BadRequest(new OperationError(error.Message));
        }

        protected async Task<ActionResult> ExecuteCommand(IRequest<Result> command)
        {
            try
            {
                var result = await Mediator.Send(command);

                return result.Succeeded ? Ok() : HandleError(result.Error);
            }
            catch (ValidationException exception)
            {
                return BadRequest(new ValidationError(exception.Failures));
            }
        }

        protected async Task<ActionResult<T>> ExecuteCommand<T>(IRequest<Result<T>> command)
        {
            try
            {
                var result = await Mediator.Send(command);

                return result.Succeeded ? Ok(result.Value) : HandleError(result.Error);
            }
            catch (ValidationException exception)
            {
                return BadRequest(new ValidationError(exception.Failures));
            }
        }

        protected async Task<ActionResult<T>> ExecuteCommand<T>(IRequest<T> command)
        {
            try
            {
                var result = await Mediator.Send(command);
                return Ok(result);
            }
            catch (ValidationException exception)
            {
                return BadRequest(new ValidationError(exception.Failures));
            }
        }
    }
}
