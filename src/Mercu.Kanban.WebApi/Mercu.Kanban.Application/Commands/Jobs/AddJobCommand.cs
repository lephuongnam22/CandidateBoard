using AutoMapper;
using MediatR;
using Mercu.Kanban.Application.DTOS;
using Mercu.Kanban.Domain.Entities;
using Mercu.Kanban.Infrastructure.UnitOfWorks;

namespace Mercu.Kanban.Application.Commands.Jobs
{
    public record AddJobCommand(JobModel JobModel) : IRequest<JobModel>;

    public class AddJobCommandHandler : IRequestHandler<AddJobCommand, JobModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddJobCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<JobModel> Handle(AddJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _unitOfWork.JobRepository.Get(n => n.JobTitle == request.JobModel.JobTitle);

            if(job != null)
            {
                throw new Exception($"Job with name {request.JobModel.JobTitle} already exist");
            }

            job = _mapper.Map<Job>(request.JobModel);

            await _unitOfWork.JobRepository.AddAsync(job);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<JobModel>(job);
        }
    }
}
