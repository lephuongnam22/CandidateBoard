using AutoMapper;
using Kanban.Board.Application.DTOS;
using Kanban.Board.Infrastructure.UnitOfWorks;
using MediatR;

namespace Kanban.Board.Application.Queries.Interviewers
{
    public record GetInterviewerQuery : IRequest<IList<InterviewerModel>>;

    public class GetInterviewerQueryHandler : IRequestHandler<GetInterviewerQuery, IList<InterviewerModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetInterviewerQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<InterviewerModel>> Handle(GetInterviewerQuery request,
            CancellationToken cancellationToken)
        {
            var interviewers = await _unitOfWork.InterviewerRepository.GetAll();

            if (interviewers.Any())
            {
                return _mapper.Map<IList<InterviewerModel>>(interviewers);
            }

            return new List<InterviewerModel>();
        }
    }   
}
