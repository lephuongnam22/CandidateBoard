using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Mercu.Kanban.WebApi.Controllers
{
    public class BaseController : ControllerBase
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public BaseController()
        {
        }

        protected virtual IActionResult HandleProblemReturn(Exception ex, string methodName)
        {
            var errMsg = ExceptionHelper(ex);
            HttpContext.Response.StatusCode = 500;
            return Problem(errMsg, methodName, 500, "An error occurred while processing your request", "Error");
        }

        private string ExceptionHelper(Exception e)
        {
            StringBuilder errMsg = new StringBuilder();

            while (e != null)
            {
                errMsg.Append(e.Message + " ");
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                e = e.InnerException;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            }

            return errMsg.ToString();
        }
    }
}
