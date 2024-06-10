using AutoMapper;
using MediatR;
using Kanban.Board.Application.DTOS;
using Kanban.Board.Domain.Entities;
using Kanban.Board.Infrastructure.UnitOfWorks;

namespace Kanban.Board.Application.Commands.Candidates
{
    public record UpdateCandidateCommand(UpdateCandidateRequest UpdateCandidateRequest) : IRequest<CandidateModel>;

    public class UpdateCandidateCommandHandler : IRequestHandler<UpdateCandidateCommand, CandidateModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCandidateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CandidateModel> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate =
                await _unitOfWork.CandidateRepository.Get(n => n.Id == request.UpdateCandidateRequest.Id);

            if (candidate == null)
            {
                throw new Exception($"Candidate with Id {request.UpdateCandidateRequest.Id} not exist");
            }

            // Check email is unique or not
            if (request.UpdateCandidateRequest.OldEmail != request.UpdateCandidateRequest.Email)
            {
                var candidateWithEmail = await _unitOfWork.CandidateRepository.Get(n => n.Email == request.UpdateCandidateRequest.Email);

                if (candidateWithEmail != null)
                {
                    throw new Exception($"Candidate with email {request.UpdateCandidateRequest.Email} already exist");
                }
            }

            candidate.Email = request.UpdateCandidateRequest.Email;
            candidate.FirstName = request.UpdateCandidateRequest.FirstName;
            candidate.LastName = request.UpdateCandidateRequest.LastName;
            candidate.PhoneNumber = request.UpdateCandidateRequest.PhoneNumber;

            _unitOfWork.CandidateRepository.Update(candidate);

            await UpdateCandidateJobRelations(request.UpdateCandidateRequest);
            await UpdateCandidateReviewerRelation(request.UpdateCandidateRequest);

            var jobs = await _unitOfWork.JobRepository.GetWithConditions(n => request.UpdateCandidateRequest.JobIds.Contains(n.Id));
            var interviewers = await _unitOfWork.InterviewerRepository.GetWithConditions(n => request.UpdateCandidateRequest.InterviewerIds.Contains(n.Id));

            await _unitOfWork.CommitAsync();

            var result = _mapper.Map<CandidateModel>(candidate);
            result.JobModels = jobs.Select(n => _mapper.Map<JobModel>(n)).ToList();
            result.InterviewerModels = interviewers.Select(n => _mapper.Map<InterviewerModel>(n)).ToList();
            return result;

        }

        private async Task UpdateCandidateReviewerRelation(AddCandidateRequest addCandidateRequest)
        {
            var candidateInterviewerRelations = await _unitOfWork.CandidateInterviewerRelationRepository
                .GetWithConditions(n => n.CandidateId == addCandidateRequest.Id);

            var interviewerIds = addCandidateRequest.InterviewerIds;

            if (candidateInterviewerRelations.Any())
            {
                var insertCandidateInterviewerRelations = new List<CandidateInterviewerRelation>();
                var interviewerIdsInDb = candidateInterviewerRelations.Select(n => n.InterviewerId).ToList();
                var interviewerIdsNotInDb = interviewerIds.Except(interviewerIdsInDb).ToList();

                var deleteInterviewerJobRelations = candidateInterviewerRelations.Where(n => !interviewerIds.Contains(n.InterviewerId)).ToList();
                _unitOfWork.CandidateInterviewerRelationRepository.DeleteRange(deleteInterviewerJobRelations);

                foreach (var interviewerId in interviewerIdsNotInDb)
                {
                    var candidateInterviewerRelation = new CandidateInterviewerRelation
                    {
                        CandidateId = addCandidateRequest.Id,
                        InterviewerId = interviewerId
                    };

                    insertCandidateInterviewerRelations.Add(candidateInterviewerRelation);
                }

                await _unitOfWork.CandidateInterviewerRelationRepository.AddRangeAsync(
                    insertCandidateInterviewerRelations);
            }
        }

        private async Task UpdateCandidateJobRelations(AddCandidateRequest addCandidateRequest)
        {
            var candidateJobRelations = await _unitOfWork.CandidateJobRelationRepository
                .GetWithConditions(n => n.CandidateId == addCandidateRequest.Id);

            var jobIds = addCandidateRequest.JobIds;

            if (candidateJobRelations.Any())
            {
                var insertCandidateJobRelations = new List<CandidateJobRelation>();
                var jobIdsInDb = candidateJobRelations.Select(n => n.JobId).ToList();
                var jobIdsNotInDb = jobIds.Except(jobIdsInDb).ToList();

                var deleteCandidateJobRelations = candidateJobRelations.Where(n => !jobIds.Contains(n.JobId)).ToList();
                _unitOfWork.CandidateJobRelationRepository.DeleteRange(deleteCandidateJobRelations);

                foreach (var jobId in jobIdsNotInDb)
                {
                    var candidateJobRelation = new CandidateJobRelation
                    {
                        CandidateId = addCandidateRequest.Id,
                        JobId = jobId
                    };

                    insertCandidateJobRelations.Add(candidateJobRelation);
                }

                await _unitOfWork.CandidateJobRelationRepository.AddRangeAsync(insertCandidateJobRelations);

            }
        }
    }
}
