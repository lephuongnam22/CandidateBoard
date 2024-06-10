using AutoMapper;
using MediatR;
using Kanban.Board.Application.DTOS;
using Kanban.Board.Infrastructure.UnitOfWorks;

namespace Kanban.Board.Application.Queries.Jobs
{
    public record GetJobQuery : IRequest<IList<JobModel>>;

    public class GetJobQueryHandler : IRequestHandler<GetJobQuery, IList<JobModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetJobQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<JobModel>> Handle(GetJobQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _unitOfWork.JobRepository.GetAll();

            if (jobs.Any())
            {
                return _mapper.Map<IList<JobModel>>(jobs);
            }

            return new List<JobModel>();
        }
    }
}
