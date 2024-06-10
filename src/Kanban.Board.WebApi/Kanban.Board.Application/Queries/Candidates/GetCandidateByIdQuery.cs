using AutoMapper;
using MediatR;
using Kanban.Board.Application.DTOS;
using Kanban.Board.Infrastructure.UnitOfWorks;

namespace Kanban.Board.Application.Queries.Candidates
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
