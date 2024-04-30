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
            var candidate = await _unitOfWork.CandidateRepository.Get(n => n.Email == request.AddCandidateRequest.Email);

            if (candidate != null)
            {
                throw new Exception($"Candidate with email {request.AddCandidateRequest.Email} already exist");
            }

            var job = await _unitOfWork.JobRepository.Get(n => n.Id == request.AddCandidateRequest.JobId);

            if (job == null)
            {
                throw new Exception($"Job with Id {request.AddCandidateRequest.JobId} not exist");
            }

            candidate = _mapper.Map<Candidate>(request.AddCandidateRequest);

            await _unitOfWork.CandidateRepository.AddAsync(candidate);

            var jobCandidateRelation = new CandidateJobRelation
            {
                Candidate = candidate,
                JobId = job.Id,
            };

            await _unitOfWork.CandidateJobRelationRepository.AddAsync(jobCandidateRelation);

            await _unitOfWork.CommitAsync();
            var result = _mapper.Map<CandidateModel>(candidate);
            result.JobId = job.Id;
            result.JobTitle = job.JobTitle;
            return result;
        }
    }
}
