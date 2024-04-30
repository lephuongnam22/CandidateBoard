using AutoMapper;
using MediatR;
using Mercu.Kanban.Application.DTOS;
using Mercu.Kanban.Infrastructure.UnitOfWorks;

namespace Mercu.Kanban.Application.Queries.Candidates
{
    public record GetCandidateByIdQuery(int Id) : IRequest<CandidateModel>;

    public class GetCandidateByIdQueryHandler : IRequestHandler<GetCandidateByIdQuery, CandidateModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCandidateByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CandidateModel> Handle(GetCandidateByIdQuery request, CancellationToken cancellationToken)
        {
            var candidate = await _unitOfWork.CandidateRepository.Get(n => n.Id ==  request.Id);
            var result = _mapper.Map<CandidateModel>(candidate);
            return result;
        }
    }
}
