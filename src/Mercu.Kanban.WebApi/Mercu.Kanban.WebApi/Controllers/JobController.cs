using Mercu.Kanban.Application.Commands.Jobs;
using Mercu.Kanban.Application.DTOS;
using Mercu.Kanban.Application.Queries.Jobs;
using Microsoft.AspNetCore.Mvc;

namespace Mercu.Kanban.WebApi.Controllers
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
