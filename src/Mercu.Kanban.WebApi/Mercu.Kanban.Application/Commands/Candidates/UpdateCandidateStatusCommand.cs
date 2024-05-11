using MediatR;
using Mercu.Kanban.Application.DTOS;
using Mercu.Kanban.Infrastructure.UnitOfWorks;

namespace Mercu.Kanban.Application.Commands.Candidates
{
    public record UpdateCandidateStatusCommand(UpdateCandidateStatusRequest UpdateCandidateStatusRequest) 
        : IRequest<UpdateCandidateStatusRequest>;

    public class UpdateCandidateStatusCommandHandler : IRequestHandler<UpdateCandidateStatusCommand, UpdateCandidateStatusRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCandidateStatusCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateCandidateStatusRequest> Handle(UpdateCandidateStatusCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _unitOfWork.CandidateRepository.Get(n => n.Id == request.UpdateCandidateStatusRequest.Id);

            if (candidate == null)
            {
                throw new Exception($"Candidate with Id {request.UpdateCandidateStatusRequest.Id} not found");
            }

            candidate.CandidateStatus = request.UpdateCandidateStatusRequest.CandidateStatus;

            await _unitOfWork.CommitAsync();
            return request.UpdateCandidateStatusRequest;
        }
    }
}
