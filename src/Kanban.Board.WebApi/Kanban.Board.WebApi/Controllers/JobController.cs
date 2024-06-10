using Kanban.Board.Application.Commands.Jobs;
using Kanban.Board.Application.DTOS;
using Kanban.Board.Application.Queries.Jobs;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Board.WebApi.Controllers
{
    [ApiController]
    [Route("jobs")]
    public class JobController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> AddJob(JobModel jobModel)
        {
            try
            {
                var result = await Mediator.Send(new AddJobCommand(jobModel));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleProblemReturn(ex, nameof(AddJob));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetJobs()
        {
            try
            {
                var result = await Mediator.Send(new GetJobQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleProblemReturn(ex, nameof(GetJobs));
            }
        }
    }
}
