using AutoMapper;
using MediatR;
using Mercu.Kanban.Application.DTOS;
using Mercu.Kanban.Domain.Entities;
using Mercu.Kanban.Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace Mercu.Kanban.Application.Queries.Candidates
{
    public record GetCandidateByStatusQuery(CandidateStatus CandidateStatus) : IRequest<CandidateStatusModel>;

    public class GetCandidateByStatusQueryHandler : IRequestHandler<GetCandidateByStatusQuery, CandidateStatusModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCandidateByStatusQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CandidateStatusModel> Handle(GetCandidateByStatusQuery request, CancellationToken cancellationToken)
        {
            var jobQuery = from i in _unitOfWork.DatabaseContext.Candidates
                join cj in _unitOfWork.DatabaseContext.CandidateJobRelations
                    on i.Id equals cj.CandidateId
                join j in _unitOfWork.DatabaseContext.Jobs on cj.JobId equals j.Id
                           where i.CandidateStatus == request.CandidateStatus
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
                where i.CandidateStatus == request.CandidateStatus 
                group c by i.Id
                into g
                select new
                {
                    g.Key,
                    Interviewers = g.Select(n => n).ToList() ?? new List<Interviewer>()
                };

            var candidateQuery = await (from c in _unitOfWork.DatabaseContext.Candidates
                join j in jobQuery on c.Id equals j.Key
                join i in interviewQuery on c.Id equals i.Key
                where c.CandidateStatus == request.CandidateStatus
                                 select new
                {
                    Id = c.Id,
                    Email = c.Email,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    CreateDate = c.CreateDate,
                    PhoneNumber = c.PhoneNumber,
                    CandidateStatus = c.CandidateStatus,
                    JobModels = j.Jobs,
                    InterviewerModels = i.Interviewers 
                }).ToListAsync(cancellationToken);

            return new CandidateStatusModel
            {
                CandidateStatus = request.CandidateStatus,
                Candidates = candidateQuery.Select(n => new CandidateModel
                {
                    Id = n.Id,
                    Email = n.Email,
                    FirstName = n.FirstName,
                    LastName = n.LastName,
                    CreateDate = n.CreateDate,
                    PhoneNumber = n.PhoneNumber,
                    CandidateStatus = n.CandidateStatus,
                    JobModels = _mapper.Map<IList<JobModel>>(n.JobModels),
                    InterviewerModels =  _mapper.Map<IList<InterviewerModel>>(n.InterviewerModels),
                })
            };
        }
    }
}
