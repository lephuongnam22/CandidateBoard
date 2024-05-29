using AutoMapper;
using MediatR;
using Mercu.Kanban.Application.DTOS;
using Mercu.Kanban.Domain.Entities;
using Mercu.Kanban.Infrastructure.UnitOfWorks;

namespace Mercu.Kanban.Application.Commands.Candidates
{
    public record AddCandidateCommand(AddCandidateRequest AddCandidateRequest) : IRequest<CandidateModel>;

    public class AddCandidateCommandHandler : IRequestHandler<AddCandidateCommand, CandidateModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddCandidateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CandidateModel> Handle(AddCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate =
                await _unitOfWork.CandidateRepository.Get(n => n.Email == request.AddCandidateRequest.Email);

            if (candidate != null)
            {
                throw new Exception($"Candidate with email {request.AddCandidateRequest.Email} already exist");
            }

            var jobs = await ValidateJobIdExist(request.AddCandidateRequest.JobIds);
            var interviewers = await ValidateInterviewerIdsExist(request.AddCandidateRequest.InterviewerIds);

            candidate = _mapper.Map<Candidate>(request.AddCandidateRequest);

            await _unitOfWork.CandidateRepository.AddAsync(candidate);

            await CreateCandidateJobRelations(candidate, jobs);
            await CreateCandidateInterviewRelation(candidate, interviewers);
            await _unitOfWork.CommitAsync();

            var result = _mapper.Map<CandidateModel>(candidate);
            result.JobModels = jobs.Select(n => _mapper.Map<JobModel>(n)).ToList();
            result.InterviewerModels = interviewers.Select(n => _mapper.Map<InterviewerModel>(n)).ToList();
            return result;
        }

        private async Task CreateCandidateInterviewRelation(Candidate candidate, IList<Interviewer> interviewers)
        {
            var candidateInterviewerRelations = new List<CandidateInterviewerRelation>();

            foreach (var interviewer in interviewers)
            {
                var candidateInterviewerRelation = new CandidateInterviewerRelation
                {
                    Candidate = candidate,
                    InterviewerId = interviewer.Id,
                };

                candidateInterviewerRelations.Add(candidateInterviewerRelation);
            }

            await _unitOfWork.CandidateInterviewerRelationRepository.AddRangeAsync(candidateInterviewerRelations);
        }


        private async Task CreateCandidateJobRelations(Candidate candidate, IList<Job> jobs)
        {
            var jobCandidateRelations = new List<CandidateJobRelation>();

            foreach (var job in jobs)
            {
                var jobCandidateRelation = new CandidateJobRelation
                {
                    Candidate = candidate,
                    JobId = job.Id,
                };

                jobCandidateRelations.Add(jobCandidateRelation);
            }

            await _unitOfWork.CandidateJobRelationRepository.AddRangeAsync(jobCandidateRelations);
        }

        private async Task<IList<Job>> ValidateJobIdExist(IList<int> jobIds)
        {
            var jobs = await _unitOfWork.JobRepository.GetWithConditions(n => jobIds.Contains(n.Id));

            if (jobs == null || !jobs.Any())
            {
                throw new Exception($"Jobs with Id {string.Join(",", jobIds)} not exist");
            }

            return jobs;
        }

        private async Task<IList<Interviewer>> ValidateInterviewerIdsExist(IList<int> interviewerIds)
        {
            var interviewers =
                await _unitOfWork.InterviewerRepository.GetWithConditions(n => interviewerIds.Contains(n.Id));

            if (interviewers == null || !interviewers.Any())
            {
                throw new Exception($"Interviewers with Id {string.Join(",", interviewerIds)} not exist");
            }

            return interviewers;
        }
    }
}
