using AutoMapper;
using MediatR;
using Mercu.Kanban.Application.DTOS;
using Mercu.Kanban.Domain.Entities;
using Mercu.Kanban.Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace Mercu.Kanban.Application.Queries.Candidates
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
           var query = from c in _unitOfWork.DatabaseContext.Candidates
                       join s in _unitOfWork.DatabaseContext.CandidateJobRelations 
                       on c.Id equals s.CandidateId into cs
                       from cs1 in cs.DefaultIfEmpty()
                       join j in _unitOfWork.DatabaseContext.Jobs 
                       on cs1.JobId equals j.Id into js
                       from js1 in js.DefaultIfEmpty()
                       select new { Candidate = c, Job = js1 };

            var groupQuery = await (from i in query
                                    group i by new
                                    {
                                        i.Candidate.CandidateStatus
                                    } into g
                                    select new CandidateStatusModel
                                    {

                                        CandidateStatus = g.Key.CandidateStatus,

                                        Candidates = g.Select(n => new CandidateModel
                                        {
                                            Id = n.Candidate.Id,
                                            FirstName = n.Candidate.FirstName,
                                            Email = n.Candidate.Email,
                                            LastName = n.Candidate.LastName,
                                            PhoneNumber = n.Candidate.PhoneNumber,
                                            JobId = n.Job != null ? n.Job.Id : 0,
                                            CandidateStatus = n.Candidate.CandidateStatus,
                                            JobTitle = n.Job != null ? n.Job.JobTitle : string.Empty
                                        }).ToList()

                                    }).ToListAsync(cancellationToken);
            return groupQuery;
        }
    }
}