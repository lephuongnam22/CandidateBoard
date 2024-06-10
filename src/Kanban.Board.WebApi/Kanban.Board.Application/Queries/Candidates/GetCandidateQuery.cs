using AutoMapper;
using MediatR;
using Kanban.Board.Application.DTOS;
using Kanban.Board.Domain.Entities;
using Kanban.Board.Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Board.Application.Queries.Candidates
{
    public record GetCandidateQuery() : IRequest<IList<CandidateStatusModel>>;

    public class GeCandidateQueryHandler : IRequestHandler<GetCandidateQuery, IList<CandidateStatusModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GeCandidateQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<CandidateStatusModel>> Handle(GetCandidateQuery request, CancellationToken cancellationToken)
        {
            var jobQuery = from i in _unitOfWork.DatabaseContext.Candidates
                join cj in _unitOfWork.DatabaseContext.CandidateJobRelations
                    on i.Id equals cj.CandidateId
                join j in _unitOfWork.DatabaseContext.Jobs on cj.JobId equals j.Id
                group j by i.Id
                into g
                select new
                {
                    g.Key,
                    Jobs = g.Select(n => n).ToList() ?? new List<Job>()
                };

            var interviewQuery = from i in _unitOfWork.DatabaseContext.Candidates
                join ci in _unitOfWork.DatabaseContext.CandidateInterviewerRelations
                    on i.Id equals ci.CandidateId
                join c in _unitOfWork.DatabaseContext.Interviewers on ci.InterviewerId equals c.Id
                group c by i.Id
                into g
                select new
                {
                    g.Key,
                    Interviewers = g.Select(n => n).ToList() ?? new List<Interviewer>()
                };

            var candidateQuery = from c in _unitOfWork.DatabaseContext.Candidates
                join j in jobQuery on c.Id equals j.Key
                join i in interviewQuery on c.Id equals i.Key
                select new
                {
                   c.Id,
                   c.Email,
                   c.FirstName,
                   c.LastName,
                   c.CreateDate,
                   c.PhoneNumber,
                   c.CandidateStatus,
                   j.Jobs,
                   i.Interviewers
                };

            var candidate = await candidateQuery.ToListAsync(cancellationToken);

            var result = (from c in candidate
                group c by c.CandidateStatus
                into g
                select new CandidateStatusModel
                {
                    CandidateStatus = g.Key,
                    Candidates = g.Select(n => new CandidateModel
                    {
                        Id = n.Id,
                        Email = n.Email,
                        FirstName = n.FirstName,
                        LastName = n.LastName,
                        CreateDate = n.CreateDate,
                        PhoneNumber = n.PhoneNumber,
                        CandidateStatus = n.CandidateStatus,
                        JobModels = n.Jobs.Select(j => _mapper.Map<JobModel>(j)).ToList(),
                        InterviewerModels = n.Interviewers.Select(i => _mapper.Map<InterviewerModel>(i)).ToList()
                    }).ToList()
                }).ToList();

            return result;
        }
    }
}