using AutoMapper.Configuration.Annotations;
using Mercu.Kanban.Application.Commands.Candidates;
using Mercu.Kanban.Application.DTOS;
using Mercu.Kanban.Application.Queries.Candidates;
using Mercu.Kanban.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Mercu.Kanban.WebApi.Controllers
{
    [ApiController]
    [Route("candidates")]
    public class CandidateController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCandidate()
        {
            try
            {
                var results = await Mediator.Send(new GetCandidateQuery());
                return Ok(results);
            }
            catch (Exception ex)
            {
                return HandleProblemReturn(ex, nameof(GetAllCandidate));
            }
        }

        [HttpGet("candidate-status")]
        public IActionResult GetCandidateStatus()
        {
            try
            {
                var result=  Enum.GetNames(typeof(CandidateStatus)).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleProblemReturn(ex, nameof(GetCandidateStatus));
            }
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetCandidateById(int id)
        {
            try
            {
                var result = await Mediator.Send(new GetCandidateByIdQuery(id));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleProblemReturn(ex, nameof(GetCandidateById));
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCandidate(AddCandidateRequest addCandidateRequest)
        {
            try
            {
                var result = await Mediator.Send(new AddCandidateCommand(addCandidateRequest));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleProblemReturn(ex, nameof(AddCandidate));
            }
        }
    }
}
