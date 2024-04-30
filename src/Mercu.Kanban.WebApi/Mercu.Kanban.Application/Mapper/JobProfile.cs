using AutoMapper;
using Mercu.Kanban.Application.DTOS;
using Mercu.Kanban.Domain.Entities;

namespace Mercu.Kanban.Application.Mapper
{
    public class JobProfile : Profile
    {
        public JobProfile()
        {
            CreateMap<Job, JobModel>().ReverseMap();
        }
    }
}
