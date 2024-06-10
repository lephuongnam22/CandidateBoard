using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mercu.Kanban.Application.DTOS;
using Mercu.Kanban.Domain.Entities;
using Mercu.Kanban.Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Int16 = System.Int16;

namespace Mercu.Kanban.Application.Queries.Candidates
{
    public record SearchCandidateQuery(SearchCandidateRequest SearchCandidateRequest) : IRequest<IList<CandidateStatusModel>>;

    public class SearchCandidateQueryHandler : IRequestHandler<SearchCandidateQuery, IList<CandidateStatusModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SearchCandidateQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<CandidateStatusModel>> Handle(SearchCandidateQuery request,
            CancellationToken cancellationToken)
        {
            var baseQuery = from c in _unitOfWork.DatabaseContext.Candidates
                join cj in _unitOfWork.DatabaseContext.CandidateJobRelations
                    on c.Id equals cj.CandidateId
                join j in _unitOfWork.DatabaseContext.Jobs
                    on cj.JobId equals j.Id
                join ci in _unitOfWork.DatabaseContext.CandidateInterviewerRelations
                    on c.Id equals ci.CandidateId
                join i in _unitOfWork.DatabaseContext.Interviewers
                    on ci.InterviewerId equals i.Id
                select new
                {
                    c.Id,
                    c.FirstName,
                    c.LastName,
                    c.Email,
                    c.CandidateStatus,
                    c.PhoneNumber,
                    c.CreateDate,
                    JobId = j.Id,
                    j.JobTitle,
                    j.JobDescription,
                    InterviewerId = i.Id,
                    InterviewerName = i.Name,
                    FullName = c.FirstName + " " + c.LastName
                };

            var filterQuery = baseQuery;

            if (!string.IsNullOrEmpty(request.SearchCandidateRequest.CandidateName))
            {
                filterQuery = filterQuery.Where(n => n.FullName.Contains(request.SearchCandidateRequest.CandidateName));
            }

            if (request.SearchCandidateRequest.InterviewerIds.Any())
            {
                filterQuery = filterQuery.Where(
                    n => request.SearchCandidateRequest.InterviewerIds.Contains(n.InterviewerId));
            }

            if (request.SearchCandidateRequest.FromDate.HasValue)
            {
                filterQuery = filterQuery.Where(n => n.CreateDate >= request.SearchCandidateRequest.FromDate);
            }

            if (request.SearchCandidateRequest.ToDate.HasValue)
            {
                filterQuery = filterQuery.Where(n => n.CreateDate <= request.SearchCandidateRequest.ToDate);
            }

            var candidateIdsQuery = filterQuery.Select(n => n.Id).Distinct();

            var resultQuery = from i in candidateIdsQuery
                join i1 in baseQuery
                    on i equals i1.Id
                select i1;


            var results = await resultQuery.ToListAsync(cancellationToken);

            var dicResult = new List<CandidateStatusModel>();

            foreach (var enumValue in Enum.GetValues<CandidateStatus>())
            {
                dicResult.Add(new CandidateStatusModel
                {
                    CandidateStatus = enumValue,
                    Candidates = new List<CandidateModel>()
                });
            }

            CandidateModel? candidateModel = null;

            foreach (var item in results)
            {
                var candidateStatusModel = dicResult.FirstOrDefault(n => n.CandidateStatus == item.CandidateStatus);

                candidateModel = candidateStatusModel?.Candidates.FirstOrDefault(n => n.Id == item.Id);

                if (candidateModel != null)
                {
                    if (candidateModel.JobModels.All(n => n.Id != item.JobId))
                    {
                        candidateModel.JobModels.Add(new JobModel
                        {
                            Id = item.JobId,
                            JobTitle = item.JobTitle,
                            JobDescription = item.JobDescription
                        });
                    }

                    if (candidateModel.InterviewerModels.All(n => n.Id != item.InterviewerId))
                    {
                        candidateModel.InterviewerModels.Add(new InterviewerModel
                        {
                            Id = item.InterviewerId,
                            Name = item.InterviewerName
                        });
                    }
                }
                else
                {
                    candidateModel = new CandidateModel
                    {
                        Id = item.Id,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Email = item.Email,
                        PhoneNumber = item.PhoneNumber,
                        CreateDate = item.CreateDate,
                        CandidateStatus = item.CandidateStatus,
                        JobModels = new List<JobModel>
                        {
                            new JobModel
                            {
                                Id = item.JobId,
                                JobTitle = item.JobTitle,
                                JobDescription = item.JobDescription
                            }
                        },
                        InterviewerModels = new List<InterviewerModel>
                        {
                            new InterviewerModel
                            {
                                Id = item.InterviewerId,
                                Name = item.InterviewerName
                            }
                        }
                    };

                    candidateStatusModel.Candidates.Add(candidateModel);
                }
            }

            return dicResult;
        }
    }
}
