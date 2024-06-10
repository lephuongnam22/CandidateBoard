using Kanban.Board.Application.Queries.Interviewers;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Board.WebApi.Controllers
{
    [ApiController]
    [Route("interviewers")]
    public class InterviewerController: BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetInterviewers()
        {
            try
            {
                var result = await Mediator.Send(new GetInterviewerQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleProblemReturn(ex, nameof(GetInterviewers));
            }
        }
    }
}
